   M S B A M L   `   `   `  ����A  =Diplom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DiplomDiplom  5   	       Diplom.AddOrderWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Diplom     5   	     � Title$/  )Редактирование заказа��5   	   $	��370��6'   $	��370��64   $	��750��6D   $	��700��6P    � WindowStartupLocation$ CenterScreen=�6_   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��0��6,   $��0,0,0,10q�69   .+�5      �� $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $"��Номер договора:��6   $	��130��62   $��Center=�6>   �� 5      -   5         ��Name$ TxtContractNumber��5      $	��200��65    �� 
IsReadOnly$
 True��6A   5      �� 5      $ 
Horizontal=�6   $	��0,5q�62   .+�5      �� $0��*Сотрудник-исполнитель:��6   $	��150��69   $��Center=�6E   �� 5      -   5         ��Name$ CmbEmployees��5      $	��200��61    �� DisplayMemberPath$ FullName��6=    �� SelectedValuePath$ 
EmployeeID��5      	 �� 
IsEditable$
	 True��6k   �� 5      -   5        $#��Новый сотрудник��5      $	��120��63   $��10,0,0,0q�6?   6Q   5      �� 5       $ 
Horizontal=�6   $	��0,5q�62   .+�5!      �� $��Дата начала:��6   $	��130��6/   $��Center=�6;   (  "System.Windows.Controls.DatePicker  5"      -   5        
  Name$
 DpStartDate��5"      $	��200��62   �� 5#      $"��Дата окончания:��6   $	��130��62   $��Center=�6>   $��20,0,0,0q�6Y     5$      -   5        $
 	DpEndDate��5$      $	��200��60   5%      �� 5(      $ 
Horizontal=�6   $	��0,5q�62   .+�5)      �� $��Выполнено:��6   $��80��6-   $��Center=�68   �� 5*      -   5         ��Name$ 	IsDoneChk��5*      $	��5,0q�6.   $��Center=�6;   5+      �� 5.      $ 
Horizontal=�6   $��0,30,0,5q�62   .+�5/      �� $$��Название заказа:��6   $	��130��63   $��Center=�6?   �� 50      -   5        $ TxtOrderName��50      $	��500��60   51      �� 54      $ 
Horizontal=�6   $	��0,5q�62   .+�55      �� $��Примечания:��6   $	��130��6.   $	��Top=�6:   �� 56      -   5        $ TxtNotes��56      $	��500��6,   $��60��68    �� TextWrapping$
 Wrap=�6D    �� AcceptsReturn$
 True��6X   57      58      �� 5;   
   $ 
Horizontal=�6   $��Right=�6.   $��2��6J   .+�5<      �� -	   5        $��Сохранить��5<      $	��100��6)   $��5q�65   6@   �� 5=      -
   5        $��Отмена��5=      $��80��6&   $��5q�61   6<   5>      5?      5@      