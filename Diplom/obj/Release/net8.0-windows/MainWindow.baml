   M S B A M L   `   `   `  ���� A  =Diplom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DiplomDiplom      Diplom.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Diplom     -     � Title$  Главное окно��$	��700��$
��1200�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0�� �� Orientation$ 
Horizontal=�$��Center=�$��0,0,0,5q�.+��� -    ��Name$ SearchTypeComboBox��$	��100��$��0,0,10,0q� �� SelectedIndex$ 0��.@��� $��По заказу�� �� 
IsSelected$
 True���� $��По сотруднику���� -    ��Name$ SearchTextBox��$	��200��$��0,0,10,0q�$��Center=� � ToolTip$, &Введите текст поиска��$�� ���� -   	 ��Name$	 SearchButton��$��
Поиск��$��80��$��	0,0,200,0q��� $ 
Horizontal=�$��Right=�.+��� -   $	 BtnCreateContract��$��+заказ��$��70��$��5q��� -   $	 BtnDeleteContract��$��-заказ��$��70��$��5,5,50,5q��� -   $	 BtnCreateEmployee��$��+сотрудник��$��70��$��5q��� -   $	 BtnDeleteEmployee��$��-сотрудник��$��70��$��5q�� $��1��
 � ColumnDefinitions
 �� $��2* ��� $��* �.+�&   System.Windows.Controls.DataGrid  -	     Name$ MainDataGrid��$��0��   AutoGenerateColumns$ False��   
IsReadOnly$
 True��   CanUserAddRows$ False�� �� AlternationCount$ 2��$��5q�   SelectionMode$ Single=�   RowStyle ��  �� 
TargetType)  #System.Windows.Controls.DataGridRow"   �� Triggers ��  �� Binding �� *OrderStatus+ �� Value	 false.a���  �� Property � �� �� Value
 �������  �� *OrderStatus+ true.a���  � ��
 ����   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$	 №��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	OrderID+  $ 
Заказ��$ 2*  �� *	OrderName+  $ Исполнитель��$ 2*  �� *EmployeeName+ � ContextMenu �� -
    ��Name$ MainDataGridContextMenu��.@�m� -   $,��&Открыть папку заказа���� $��1��$��5q�$ Vertical=�.+��� $��Примечания:��$
��Bold"�$��0,0,0,5q��� -   $ NotesTextBox��$	��300�� �� 
IsReadOnly$
 True�� �� TextWrapping$
 Wrap=�$��0,0,0,10q�