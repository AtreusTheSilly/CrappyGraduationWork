   M S B A M L   `   `   `  ����
  Diplom    Diplom.EmployeeListWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$)  #Список сотрудников��5   	   $	��500��6$   $	��800��61    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � $��10q�6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ EmployeeDataGrid��5      $��0��5         AutoGenerateColumns$ False��5	         
IsReadOnly$
 True��5
         CanUserAddRows$ False��5       �� AlternationCount$ 2��5      $��5q�5         SelectionMode$ Single=�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *
EmployeeID+6P     5      $	 ФИО��6%   $
 * 6O    62   �� *
FullName+6O     5      $	 Телефон��6%   $
 * 6V    66   �� *NumberPhone+6V     5      $	 Email��6%   $
 * 6N    64   �� *Email+6N   5      5      5      