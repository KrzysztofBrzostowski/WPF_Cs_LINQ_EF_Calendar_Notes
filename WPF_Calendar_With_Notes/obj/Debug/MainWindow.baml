   M S B A M L   `   `   `  ����R  NWPF_Calendar_With_Notes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:WPF_Calendar_With_Notes.Utilities.Converters,WPF_Calendar_With_Notes.Utilities.Converters  5   	   U/clr-namespace:WPF_Calendar_With_Notes.ViewModel!WPF_Calendar_With_Notes.ViewModel  5   	   (    "WPF_Calendar_With_Notes.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Q
Converters:clr-namespace:WPF_Calendar_With_Notes.Utilities.Converters     5   	   >VM/clr-namespace:WPF_Calendar_With_Notes.ViewModel     5   	   $	��520��5   	   $	��800��6     � Title  5   	   �� *MainWindowTitle+   	Resources �� Source# [   � 	Resources 5	      B   <WPF_Calendar_With_Notes.Utilities.Converters.ConverterOfDate  StatusBarDateConverterB  <WPF_Calendar_With_Notes.Utilities.Converters.ConverterOfHour  InstanceOfHourD  >WPF_Calendar_With_Notes.Utilities.Converters.ConverterOfMinute  InstanceOfMinuteB  <WPF_Calendar_With_Notes.Utilities.Converters.ConverterOfNote  InstanceOfNoteS  MWPF_Calendar_With_Notes.Utilities.Converters.DataGridDescriptionDateConverter $  DataGridDescriptionDateConverterI   CWPF_Calendar_With_Notes.Utilities.Converters.NumberOfNotesConverter  NumberOfNotesConverter��%�   &	       &	      &	 &     &	 9     &	 L     &	 _       5
   
   6%     5   
   6%     5   
   6'     5   
   6%     5   
   66     5   
   6,   5      .��5      ]� .+�5   
   o� -   5         o�Name$ menu1��5      $��23��6   $	��Top=�6*   .@�5      m� ��6   �� *File+# [  .@�5      m� -   5        ��5      �� *Exit+# [  6U   5      m� 5      ��6   �� *	Options+# [  .@�5      m� ��6   �� *
Language+# [  .@�5      m� $��EN��6    m� CommandParameter$ en-GB��6U    m� Command 6+   �� *LanguageChangeCommand+6U   m� 5      $��PL��6   $ pl-PL��6U    6+   �� *LanguageChangeCommand+6U   5      5      m� 5!      ��6   �� *Help+# [  .@�5"      m� ��6   �� *About+# [   6W   �� *HelpCommand+5#      5%      �� 5(   
   -   5         ��Name$ 
statusBar1��5(      $��Bottom=�6(   .@�5*      �� .��5+      �� -   5         ��Name$ DateTextBlock��5-   I   $	��235��5+      $��12(�6(   ��66   ��  �� ElementName$ MainCalendar��	 �� Path$	 SelectedDate�
 �� 	Converter#
 [ 5-   I   5.      �� 50      $��2��6   �� 52      .��53      �� -   5        $ 
textBlock1��53   (   $	��145��6   ��6<   �� $(	 "Engine.NumberOfPositionForDataGrid�#
 [ 54      �� 56      $��2��6   �� 58      .��59      �� -   5        $ tbDataBaseState��59   (   $	��145��6   ��6A   �� $	 Engine.DataBaseState�5:      �� 5<      $��2��6   �� 5>      .��5?      �� -   5        $ 
textBlock2��5?   F   ��6   �� $	 Engine.LanguageName�6F   5@      5B      � 5E   
   .+�5F      &   System.Windows.Controls.Calendar  -   5        $��MainCalendar��5I         SelectionMode$ MultipleRange=�5G         IsTodayHighlighted . $	��165��5H   1   $	��180��6>   $��	24,55,0,0q�6J   $	��Top=�5I   +   $
��Left=�5K         SelectedDate 5F      �� $	 Engine.Selected_Date� �� Mode$ TwoWay=�5K      �� 5M      -	   5        $��label1��5N   #   $��30��5M   W   $	��110��6c   $��Tahoma)�6o   $��14(�6�   $
��Left=�6�   $��	71,24,0,0q�5N      $	��Top=�61   ��5M      �� *ChoosingDay+# [  5N   1   �� 5P      -
   5        $��lDataGridDescription��5U   Q   $��30��5T      $	��307��6    $��Tahoma)�6,   $��14(�5U      $
��Left=�6"   $��
368,24,0,0q�6=   $	��Top=�6m   ��5P      �� $ MainCalendar��$	 SelectedDate�#
 [ 5U   m   &   System.Windows.Controls.DataGrid  5W      -   5        $��	dataGrid1��5[   O      
IsReadOnly .     AutoGenerateColumns .  $
��Left=�5Z      $	��330��5[      $��
230,55,0,0q�6!   $	��520��65   $��12(�6A   $	��Top=�6`   ��5Y   *   �� $	 Engine.Positions�   Columns 5\      0	  *System.Windows.Controls.DataGridTextColumn	  5]      ,
  &System.Windows.Controls.DataGridColumn 
  Width5  /System.Windows.Controls.DataGridLengthConverter$ 38 5a   6    
  Header 5]   )   �� *Hour+# [  1  +System.Windows.Controls.DataGridBoundColumn   Binding 5^   )   �� $	 CurrentHour� �� UpdateSourceTrigger$ PropertyChanged=�#
 [  �� ValidatesOnDataErrors . 5a   6   	  5c      $ 35 5g   6    5c   )   �� *Minute+# [   5d   )   �� $	 CurrentMinute�$ PropertyChanged=�#
 [  . 5g   6   	  5i       
  MinWidth$	 340Q�5m   6    5i   )   �� *Note+# [   5j   )   �� $	 CurrentNote�$ PropertyChanged=�#
 [  . 5m   6   	  5o      $ 90Q�5s   6    5o   )   �� *User+# [   5p   )   �� $	 CurrentUser�$ PropertyChanged=�#
 [  . 5s   6   5x      �� 5{      -   5        $��bEditSelected��5{   t   $
��Left=�6Y   $	��Top=�6�   $��12(�6�   $��24��5|      $��422,390,0,0q�6   $	��130��64   ��5{      �� *EditSelected+# [  ��5|   @   �� *EditSelectedNoteCommand+�� 5~      -   5        $��bDeleteSelectedNote��5~   z   $
��Left=�6_   $	��Top=�6�   $��12(�6�   $��24��5      $��620,390,0,0q�6   $	��130��64   ��5~      �� *RemoveSelectedNote+# [  ��5   @   �� *DeleteSelectedNoteCommand+�� 5�      -   5        $��bAddNote��5�   r   $
��Left=�6W   $	��Top=�6�   $��12(�6�   $��24��6�   $��230,390,0,0q�6�   $	��130��6�   ��6   �� *
AddNewNote+# [  ��6�   �� *NewNoteCommand+5�      5�      5�      