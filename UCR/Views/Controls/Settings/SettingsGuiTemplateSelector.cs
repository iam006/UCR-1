﻿using System;
using System.Windows;
using System.Windows.Controls;
using HidWizards.UCR.Core.Models;
using HidWizards.UCR.ViewModels.ProfileViewModels;

namespace HidWizards.UCR.Views.Controls.Settings
{
    public class SettingsGuiTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element == null || !(item is SettingsPropertyViewModel)) return null;
            var settingsProperty = ((SettingsPropertyViewModel) item).SettingsProperty;

            switch (Type.GetTypeCode(settingsProperty.PropertyInfo.PropertyType))
            {
                case TypeCode.Boolean:
                    return element.FindResource("BooleanTemplate") as DataTemplate;
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    if (settingsProperty.Property is Enum)
                    {
                        return element.FindResource("EnumTemplate") as DataTemplate;
                    }
                    return element.FindResource("NumberTemplate") as DataTemplate;
                case TypeCode.Decimal:
                case TypeCode.Double:
                    return element.FindResource("DecimalTemplate") as DataTemplate;
                case TypeCode.String:
                    return element.FindResource("StringTemplate") as DataTemplate;
                case TypeCode.Object:
                    if (settingsProperty.PropertyInfo.PropertyType == typeof(Guid))
                    {
                        return element.FindResource("StateTemplate") as DataTemplate;
                    }
                    return null;

                default:
                    return null;
            }

            
        }
    }
}