using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Diplom.Models;

namespace Diplom
{
    class WordProcessor
    {
        /// <summary>
        /// Открывает документ и рекурсивно проходит по всем элементам,
        /// обрабатывая найденные абзацы для замены тегов.
        /// Также обрабатываются заголовки и колонтитулы.
        /// </summary>
        public static void ReplaceTags(string filePath, Dictionary<string, string> replacements)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, true))
            {
                // Обрабатываем основной документ
                ProcessElement(wordDoc.MainDocumentPart.Document, replacements);

                // Обрабатываем заголовки (если они есть)
                if (wordDoc.MainDocumentPart.HeaderParts != null)
                {
                    foreach (var header in wordDoc.MainDocumentPart.HeaderParts)
                    {
                        ProcessElement(header.Header, replacements);
                    }
                }

                // Обрабатываем колонтитулы (если они есть)
                if (wordDoc.MainDocumentPart.FooterParts != null)
                {
                    foreach (var footer in wordDoc.MainDocumentPart.FooterParts)
                    {
                        ProcessElement(footer.Footer, replacements);
                    }
                }

                wordDoc.MainDocumentPart.Document.Save();
            }
        }


        /// <summary>
        /// Рекурсивно обходит все дочерние элементы указанного элемента.
        /// Если элемент является Paragraph, то он обрабатывается для замены тегов.
        /// </summary>
        private static void ProcessElement(OpenXmlElement element, Dictionary<string, string> replacements)
        {
            foreach (var child in element.ChildElements)
            {
                if (child is DocumentFormat.OpenXml.Wordprocessing.Paragraph paragraph)
                {
                    ProcessParagraph(paragraph, replacements);
                }
                // Рекурсивный вызов для всех дочерних элементов
                ProcessElement(child, replacements);
            }
        }

        /// <summary>
        /// Обрабатывает абзац. Для каждого Run-а анализируется текст посимвольно:
        /// при обнаружении начального символа '#' начинается накопление тега,
        /// и если закрывающий '#' найден – производится замена по словарю.
        /// При этом форматирование каждого Run-а сохраняется, а пробелы не теряются.
        /// Изменение: если Run содержит изображения (Drawing), он клонируется и сохраняется как есть.
        /// </summary>
        private static void ProcessParagraph(DocumentFormat.OpenXml.Wordprocessing.Paragraph paragraph, Dictionary<string, string> replacements)
        {
            List<DocumentFormat.OpenXml.Wordprocessing.Run> newRuns = new List<DocumentFormat.OpenXml.Wordprocessing.Run>();

            bool insideTag = false;            // находимся ли в теге
            string tagBuffer = "";             // накапливаем символы тега
            DocumentFormat.OpenXml.Wordprocessing.RunProperties tagStartProps = null; // форматирование первого Run-а, где начался тег

            // Буфер для обычного текста, не входящего в тег, и его форматирование
            string normalBuffer = "";
            DocumentFormat.OpenXml.Wordprocessing.RunProperties normalBufferProps = null;

            foreach (var run in paragraph.Elements<DocumentFormat.OpenXml.Wordprocessing.Run>())
            {
                // Если Run содержит изображение, сохраняем его без изменений.
                if (run.Descendants<Drawing>().Any())
                {
                    newRuns.Add((DocumentFormat.OpenXml.Wordprocessing.Run)run.CloneNode(true));
                    continue;
                }

                string runText = run.InnerText;
                DocumentFormat.OpenXml.Wordprocessing.RunProperties runProps = run.RunProperties != null
                                         ? (DocumentFormat.OpenXml.Wordprocessing.RunProperties)run.RunProperties.CloneNode(true)
                                         : null;

                for (int i = 0; i < runText.Length; i++)
                {
                    char c = runText[i];
                    if (!insideTag)
                    {
                        if (c == '#')
                        {
                            // Если есть накопленный обычный текст – фиксируем его в новый Run
                            if (normalBuffer.Length > 0)
                            {
                                newRuns.Add(CreateRun(normalBuffer, normalBufferProps));
                                normalBuffer = "";
                                normalBufferProps = null;
                            }
                            // Начинаем собирать тег
                            insideTag = true;
                            tagBuffer = "#";
                            tagStartProps = runProps != null ? (DocumentFormat.OpenXml.Wordprocessing.RunProperties)runProps.CloneNode(true) : null;
                        }
                        else
                        {
                            // Накопление обычного текста
                            normalBuffer += c;
                            if (normalBufferProps == null)
                            {
                                normalBufferProps = runProps != null 
                                    ? (DocumentFormat.OpenXml.Wordprocessing.RunProperties)runProps.CloneNode(true) 
                                    : null;
                            }
                        }
                    }
                    else // если мы накапливаем тег
                    {
                        tagBuffer += c;
                        if (c == '#' && tagBuffer.Length > 1)
                        {
                            // Если такой тег есть в replacements – заменяем его
                            if (replacements.TryGetValue(tagBuffer, out string replacement))
                            {
                                if (normalBuffer.Length > 0)
                                {
                                    newRuns.Add(CreateRun(normalBuffer, normalBufferProps));
                                    normalBuffer = "";
                                    normalBufferProps = null;
                                }
                                newRuns.Add(CreateRun(replacement, tagStartProps));
                            }
                            else
                            {
                                // Если тег не распознан – добавляем как обычный текст
                                newRuns.Add(CreateRun(tagBuffer, tagStartProps));
                            }
                            // Сбрасываем накопление тега
                            insideTag = false;
                            tagBuffer = "";
                            tagStartProps = null;
                        }
                    }
                }

                // Если не находимся внутри тега, добавляем накопленный обычный текст
                if (!insideTag && normalBuffer.Length > 0)
                {
                    newRuns.Add(CreateRun(normalBuffer, normalBufferProps));
                    normalBuffer = "";
                    normalBufferProps = null;
                }
            }

            // Если документ закончился, а тег так и не был закрыт – вставляем накопленный текст как есть
            if (insideTag && tagBuffer.Length > 0)
            {
                newRuns.Add(CreateRun(tagBuffer, tagStartProps));
            }

            // Перезаписываем абзац новыми Run-ами
            paragraph.RemoveAllChildren<DocumentFormat.OpenXml.Wordprocessing.Run>();
            foreach (var newRun in newRuns)
            {
                paragraph.Append(newRun);
            }
        }

        /// <summary>
        /// Создает новый Run с заданным текстом и форматированием.
        /// Свойство xml:space установлено в "preserve", чтобы пробелы не терялись.
        /// </summary>
        private static DocumentFormat.OpenXml.Wordprocessing.Run CreateRun(string text, DocumentFormat.OpenXml.Wordprocessing.RunProperties runProperties)
        {
            DocumentFormat.OpenXml.Wordprocessing.Run newRun = new DocumentFormat.OpenXml.Wordprocessing.Run();
            // Клонируем свойства форматирования, если они есть
            if (runProperties != null)
            {
                newRun.RunProperties = (DocumentFormat.OpenXml.Wordprocessing.RunProperties)runProperties.CloneNode(true);
            }
            // Создаем новый текстовый элемент с заданным текстом
            DocumentFormat.OpenXml.Wordprocessing.Text newText = new DocumentFormat.OpenXml.Wordprocessing.Text(text) { 
                Space = SpaceProcessingModeValues.Preserve 
            };
            // Добавляем текст в новый Run
            newRun.Append(newText);
            return newRun;
        }
    }
}
