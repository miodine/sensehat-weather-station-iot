   M S B A M L   `   `   `  ����H  DMultiView App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MultiViewApp.ViewMultiViewApp.View  5      ?$clr-namespace:MultiViewApp.ViewModelMultiViewApp.ViewModel  5          MultiViewApp.View.View1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:MultiViewApp.View     5      :	viewmodel$clr-namespace:MultiViewApp.ViewModel     5        � DataContext  5
      ,  &MultiViewApp.ViewModel.View1_ViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$
 lbl2��5      $��0��6"   $��Settings��5      $��Center=�5      $
��Left=�5      $
��15,5q�5      $��30(�5      $��DemiBold"�6"   � 5   
   $��1��6    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       $
��Auto �6    �� 5!      $
��Auto �6     � ColumnDefinitions 5%      �� 5&      $
��Auto �6#   �� 5'      $	��500 �6#   .+�5*      �� -   5        $ lbl_setting_1��5*      $��0��6/   $��0��6<   $��Server IP: ��5+      $��Center=�5,      $
��Left=�5-      $
��15,5q�5.      $��15(�5/      $��DemiBold"�6"   �� 51      -   5         ��Name$ tbx_setting_1��51      $��0��53      $��1��54      ��52      �� *	Setting_1+54      �� 57      -   5        $ lbl_setting_2��57      $��1��6/   $��0��58      $��Sample time [ms]: ��59      $��Center=�5:      $
��Left=�5;      $
��15,5q�5<      $��15(�5=      $��DemiBold"�6"   �� 5?      -   5        $ tbx_setting_2��5?      $��1��5A      $��1��5B      ��5@      �� *	Setting_2+5B      �� 5D      -   5        $ lbl_setting_3��5D      $��2��6/   $��0��5E      $��X-axis max: ��5F      $��Center=�5G      $
��Left=�5H      $
��15,5q�5I      $��15(�5J      $��DemiBold"�6"   �� 5L      -   5        $ tbx_setting_3��5L      $��2��5N      $��1��5O      ��5M      �� *	Setting_3+5O      5P      � 5T   
   $��2��6    5U      �� 5V      $
��Auto �6#   �� 5W      $
��Auto �6#   .+�5Z      �� -   5         ��Name$ btn_save��5Z      $��0��6(   $��Save settings��5[      $
��Left=�5\      $��20q�5]      $	��0,5q�6   $	��100��5^      ��5_      �� *SaveButtonCommand+�� 5a      -	   5        $ btn_load��5a      $��1��6(   $��Load settings��5b      $��Right=�5c      $��20q�5d      $	��0,5q�6   $	��100��5e      ��5f      �� *LoadButtonCommand+5g      5j      5l      