   M S B A M L   `   `   `  ���� A  =Diplom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DiplomDiplom      Diplom.AddEmployeeWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Diplom       � Title$7  1Редактирование сотрудника��$	��550��$	��500��$	��550��$	��500�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+��� -    ��Name$ PhysicalCustomerPanel��$��1��$	��0 5q�.+���  �� Orientation$ 
Horizontal=�$	��0 5q�.+��� $��ФИО:��$	��150��$��Center=��� -    ��Name$ TxtFullName��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $"��Серия паспорта:��$	��150��$��Center=��� -   $ TxtPassportSeries��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $"��Номер паспорта:��$	��150��$��Center=��� -   $ TxtPassportNumber��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $'��!Кем выдан паспорт:��$	��150��$��Center=��� -   $ TxtPassportIssuedBy��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $��Дата выдачи:��$	��150��$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ DpPassportDate��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $(��"Адрес регистрации:��$	��150��$��Center=��� -   $ TxtRegistration��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $&�� Адрес проживания:��$	��150��$��Center=��� -   $ 
TxtAddress��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $��ИНН:��$	��150��$��Center=��� -	   $ TxtINN��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $��СНИЛС:��$	��150��$��Center=��� -
   $ TxtSNILS��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $$��Почтовый индекс:��$	��150��$��Center=��� -   $ TxtPost��$	��300���� $��2��$��0 10 0 0q�.+��� $ 
Horizontal=�$	��0 5q�.+��� $��Телефон:��$	��150��$��Center=��� -   $ TxtPhone��$	��300���� $ 
Horizontal=�$	��0 5q�.+��� $��Email:��$	��150��$��Center=��� -   $ TxtEmail��$	��300���� -   $��Сохранить��$��0 10 0 0q�$	��150��$
��Left=�$��Bottom=�