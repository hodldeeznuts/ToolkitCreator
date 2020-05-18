using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ToolkitCreator
{
    public static class WidgetExtensions
    {
        public static void TextFieldNumericFloat(Rect rect, ref float val, ref string buffer)
        {
            float.TryParse(string.Format("{0:0.00}", Regex.Replace(Widgets.TextField(rect, buffer), "[^.0-9]", "")), out val);
        }

        public static void TextFieldNumericDouble(Rect rect, ref double val, ref string buffer)
        {
            double.TryParse(string.Format("{0:0.00}", Regex.Replace(Widgets.TextField(rect, buffer), "[^.0-9]", "")), out val);
        }
    }
}
