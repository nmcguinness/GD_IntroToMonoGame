﻿using System.Text.RegularExpressions;

namespace GDLibrary
{
    /// <summary>
    /// Provide useful string related functions (e.g. parse, regex)
    /// </summary>
    public class StringUtility
    {
        /// <summary>
        /// Parse a file name from a path + name string
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ParseNameFromPath(string path)
        { //"Assets/Textures/sky"
            return Regex.Match(path, @"[^\\/]*$").Value;
        }
    }
}
