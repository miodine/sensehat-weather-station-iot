   M S B A M L   `   `   `  ����H  DMultiView App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MultiViewApp.ViewMultiViewApp.View  5   	   ?$clr-namespace:MultiViewApp.ViewModelMultiViewApp.ViewModel  5   	       MultiViewApp.View.View3   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   R NOxyPlot.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5   	   +localclr-namespace:MultiViewApp.View  5   	   4	viewmodel$clr-namespace:MultiViewApp.ViewModel  5   	     � DataContext  5
      ,   &MultiViewApp.ViewModel.View3_ViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
     OxyPlot.Wpf.PlotView  -   5          Name$ DataPlotView��5      $��1��5      $��1��6$   $��0,0,10,0q�5         Model 5      �� *DataPlotModel+� 5   
   $��2��6   $��5��6    5       �� 5!      $
��Auto �6#   �� 5"      $	��100 �6#   �� 5#      $
��Auto �6#   �� 5$      $
��Auto �6#   �� 5%      $
��Auto �6#   .+�5'      �� $��2��6    �� Orientation$ 
Horizontal=�6-   .+�5(      �� $��
20,10,0,10q�5)      $��IP address: ��5*      $��Center=�6%   �� 5+      $��	0,10,0,10q�5,      $	��100��5-      $
��Left=�5.      ��5/      �� *	IpAddress+50      �� 51      $��2��6   $��2��6-   $ 
Horizontal=�6=   .+�52      �� $��10,0,0,0q�53      $��82��54      $��Sampling time: ��55      $��Center=�56      �� 57      $��	0,10,0,10q�58      $
��Left=�59      $��Center=�5:      ��5;      �� *
SampleTime+5>      �� 5@      $��4��5A      $��5,0,0,0q�5B      $��Center=�5C      $
��[ms]��5D      5E      � 5G   
   $��4��6   $��8��6    5H      �� 5I      $
��110* �6#   �� 5J      $��75 �6#   �� 5K      $��65 �6#   �� 5L      $��65 �6#   �� 5M      $��75 �6#   �� 5N      $��65 �6#   �� 5O      $��65 �6#   �� 5P      $	��100 �6#   .+�5R      �� $��0��6   .+�5S      �� $��30��5T      $	��100��5U      $��20,10.4,5.4,5.2q�5V      $��START��5X      $
��Left=�5Y      ��5W      �� *StartButton+.��5Y   -   START5[      �� 5\      $��30��5]      $	��100��5^      $��20,5.2,5.4,10.4q�5_      $
��Left=�5a      ��5`      �� *
StopButton+.��5a   ,   STOP5c      5d      �� 5e      $��1��6   $��	40,15,0,5q�6)   .+�5f      �� $��0,0,0,2q�6   $��Right=�6-   Roll:6P   �� 5g      $��0,0,0,2q�6   $��Right=�6-   Pitch:6Q   �� 5h      $��0,0,0,2q�6   $��Right=�6-   Yaw:6O   5i      �� 5j      $��2��6   $��5,15,0,5q�6)   .+�5k      �� -   5         ��Name$ rollDeg��5k   )   $	��deg��6    d� 	IsChecked 6:   �� *RollDegChecked+�� 5l      -   5        $ pitchDeg��5l   )   $	��deg��6    6;   �� *PitchDegChecked+�� 5m      -   5        $ yawDeg��5m   )   $	��deg��6    69   �� *YawDegChecked+5n      �� 5o      $��3��6   $��5,15,0,5q�6)   .+�5p      �� -   5        $ rollRad��5p   )   $	��rad��6    6:   �� *RollRadChecked+�� 5q      -   5        $ pitchRad��5q   )   $	��rad��6    6;   �� *PitchRadChecked+�� 5r      -   5        $ yawRad��5r   )   $	��rad��6    69   �� *YawRadChecked+5s      �� 5t      $��4��6   $��5,15,0,5q�6)   .+�5u      �� $��0,0,0,2q�6   $��Right=�6-   	Humidity:6T   �� 5v      $��0,0,0,2q�6   $��Right=�6-   	Pressure:6T   �� 5w      $��0,0,0,2q�6   $��Right=�6-   Temperature:6W   5x      �� 5y      $��5��6   $��5,15,0,5q�6)   .+�5z      �� -   5        $ humidityPercent��5z   '   $��%��6    6@   �� *HumidityPercentChecked+�� 5{      -	   5        $ pressureHpa��5{   )   $	��hPa��6    6>   �� *PressureHpaChecked+�� 5|      -
   5        $ temperatureC��5|   '   $��C��6    6=   �� *TemperatureCChecked+5}      �� 5      $��6��6   $��5,15,0,5q�6)   .+�5�      �� -   5        $ humidityNum��5�   '   $��-��6    6<   �� *HumidityNumChecked+�� 5�      -   5        $ pressureMmhg��5�   *   $
��mmHg��6    6@   �� *PressureMmhgChecked+�� 5�      -   5        $ temperatureF��5�   '   $��F��6    6=   �� *TemperatureFChecked+5�      5�      5�      5�      