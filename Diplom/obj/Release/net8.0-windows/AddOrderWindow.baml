   M S B A M L   `   `   `  ���� A  =Diplom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DiplomDiplom      Diplom.AddOrderWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Diplom       � Title$/  )Редактирование заказа��$	��370��$	��370��$	��750��$	��700�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto �.+���  �� Orientation$ Vertical=�$��0��$��0,0,0,10q�.+��� $ 
Horizontal=�$	��0,5q�.+��� $"��Номер договора:��$	��130��$��Center=��� -    ��Name$ TxtContractNumber��$	��200�� �� 
IsReadOnly$
 True���� $ 
Horizontal=�$	��0,5q�.+��� $0��*Сотрудник-исполнитель:��$	��150��$��Center=��� -    ��Name$ CmbEmployees��$	��200�� �� DisplayMemberPath$ FullName�� �� SelectedValuePath$ 
EmployeeID��	 �� 
IsEditable$
	 True���� -   $#��Новый сотрудник��$	��120��$��10,0,0,0q��� $ 
Horizontal=�$	��0,5q�.+��� $��Дата начала:��$	��130��$��Center=�(  "System.Windows.Controls.DatePicker  -   
  Name$
 DpStartDate��$	��200���� $"��Дата окончания:��$	��130��$��Center=�$��20,0,0,0q�  -   $
 	DpEndDate��$	��200���� $ 
Horizontal=�$	��0,5q�.+��� $��Выполнено:��$��80��$��Center=��� -    ��Name$ 	IsDoneChk��$	��5,0q�$��Center=��� $ 
Horizontal=�$��0,30,0,5q�.+��� $$��Название заказа:��$	��130��$��Center=��� -   $ TxtOrderName��$	��500���� $ 
Horizontal=�$	��0,5q�.+��� $��Примечания:��$	��130��$	��Top=��� -   $ TxtNotes��$	��500��$��60�� �� TextWrapping$
 Wrap=� �� AcceptsReturn$
 True���� $ 
Horizontal=�$��Right=�$��2��.+��� -	   $��Сохранить��$	��100��$��5q��� -
   $��Отмена��$��80��$��5q�