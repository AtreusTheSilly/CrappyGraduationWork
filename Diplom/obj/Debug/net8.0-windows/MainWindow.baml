   M S B A M L   `   `   `  ����A  =Diplom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DiplomDiplom  5   	       Diplom.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Diplom     5   	   -   5          � Title$  Главное окно��5   	   $	��700��5	   	   $
��1200��6    � WindowStartupLocation$ CenterScreen=�5
   	   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   $
��True��6!   $��0,0,0,5q�66   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��5q�62   $
��Left=�6=   $��Center=�6S   .+�5      �� -   5         ��Name$ SearchTypeComboBox��5      $	��120��5      $��0,0,10,0q�6*    �� SelectedIndex$ 0��5      .@�5      �� $��По заказу��6#    �� 
IsSelected$
 True��67   �� 5      $��По сотруднику��6#   5       �� 5"      -   5         ��Name$ SearchTextBox��5"      $	��250��5#      $��0,0,10,0q�6&   $��Center=�5$       � ToolTip$3 -Введите текст для поиска��5%      5&      5'      �� 5*      $ 
Horizontal=�6   $��Right=�62   $��Center=�6I   $��10,0,0,0q�6d   .+�5-      �� 
�������$��1q�6,   	 �� CornerRadius$	 4��6@   
 �� Padding$
 5q�6Q   $��0,0,10,0q�6]   ._�5.      �� $ 
Horizontal=�6!   .+�5/      �� -   5         ��Name$ BtnCreateContract��5/   !   $��+ заказ��6<   $��90��6N   $	��5,0q�6Y   6f   �� 50      -   5        $ BtnDeleteContract��50   !   $��- заказ��6<   $��90��6N   $	��5,0q�6Y   6f   51      52      �� 55      
�������$��1q�6,   $	 4��6@   $
 5q�6Q   $��0,0,10,0q�6]   ._�56      �� $ 
Horizontal=�6!   .+�57      �� -   5        $ BtnCreateEmployee��57   !   $��+ сотрудник��6<   $	��100��6R   $	��5,0q�6^   6k   �� 58      -   5        $ BtnDeleteEmployee��58   !   $��- сотрудник��6<   $	��100��6R   $	��5,0q�6^   6k   59      5:      �� 5=      -   5        $ BtnSeeEmployees��5=      $!��все сотрудники��62   $	��120��6K   $	��5,0q�6W   6d   5>      5@      � 5C   
   $��1��6    � ColumnDefinitions 5D      �� 5E      $��2* �6#   �� 5G      $��* �6#   .+�5L      &   System.Windows.Controls.DataGrid  -	   5          Name$ MainDataGrid��5L      $��0��5M         AutoGenerateColumns$ False��5N         
IsReadOnly$
 True��5O         CanUserAddRows$ False��5P       �� AlternationCount$ 2��5Q      $��5q�5R         SelectionMode$ Single=�5S         RowStyle 5X      �� 5Y       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5Z      �� 5[       �� Binding 6*   �� *OrderStatus+ �� Value	 false6J   .a�5\   "   ��  �� Property � �� �� Value
 �����6?   5]      �� 5^       6*   �� *OrderStatus+ true6J   .a�5_   "   ��  � ��
 ����6?   5`      5b         Columns 5f      0  *System.Windows.Controls.DataGridTextColumn  5g      ,  &System.Windows.Controls.DataGridColumn   Header$	 №��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *	OrderID+6P     5h      $ 
Заказ��6)   $ 2* 6V    68   �� *	OrderName+6V     5i      $ Исполнитель��6)   $ 2* 6_    6>   �� *EmployeeName+6_    � ContextMenu 5m      �� 5n      .@�5o      m� -
   5        $*��$Открыть файл заказа��5o   #   6@   5p      5r      �� 5u      $��1��6   $
��10,5q�6)   $��Stretch=�67   .+�5v      �� $��Примечания:��6   $
��Bold"�6/   $��0,0,0,5q�6A   �� 5w      -   5        $ NotesTextBox��5w      $	��300��5x       �� TextWrapping$
 Wrap=�5y        �� 
IsReadOnly$
  True��5z      "! �� VerticalScrollBarVisibility$
! Auto=�5{      " �� AcceptsReturn$
" True��5|      5}      5~      5      5�      