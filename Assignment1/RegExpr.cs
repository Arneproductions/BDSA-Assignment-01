using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Assignment1
{
    public static class RegExpr
    {
        /// <summary>
        /// Splits each line to only contain words
        /// </summary>
        /// <param name="lines">A collection of lines</param>
        /// <returns>A collection of words</returns>
        public static IEnumerable<string> SplitLine(IEnumerable<string> lines)
        {
            foreach (string line in lines)
                foreach (Match match in Regex.Matches(line, "[a-z,A-Z,æåøÆÅØ,0-9]+"))
                    yield return match.Value;
        }

        /// <summary>
        /// Parses the input and converts resolution patterns like 1920x1080 to tuples like (1920, 1080)
        /// </summary>
        /// <param name="resolutions">A string with tuples</param>
        /// <returns>A collection og resolution tuples</returns>
        public static IEnumerable<(int width, int height)> Resolution(string resolutions)
        {
            foreach(Match match in Regex.Matches(resolutions, @"\b(?'width'[0-9]+)x{1}(?'height'[0-9]+)\b"))
            {
                string width = match.Groups.GetValueOrDefault("width").Value;
                string height = match.Groups.GetValueOrDefault("height").Value;

                yield return new (int.Parse(width), int.Parse(height));
            }
            
        }

        /// <summary>
        /// Gets text inside from a secific tag
        /// </summary>
        /// <param name="html">The html to get inner text from</param>
        /// <param name="tag">The tag type to get text from</param>
        /// <returns>A collection of text from all of the tags found</returns>
        public static IEnumerable<string> InnerText(string html, string tag)
        {
            foreach (Match match in Regex.Matches(html, @$"<(?'tag'[a-z]+?)(\s.*?)*>(?'text'.*?)<\/\k'tag'>"))
            {
                // If it is the tag we are looking for remove all inner tags and return text
                if(match.Groups.GetValueOrDefault("tag").Value == tag)
                    yield return RemoveHtmlTags(match.Groups.GetValueOrDefault("text").Value);
                
                // Get all inner tags and return inner text
                foreach (string innerText in InnerText(match.Groups.GetValueOrDefault("text").Value, tag))
                    yield return innerText;  
            }
        }

        /// <summary>
        /// Removes tags from the string
        /// </summary>
        /// <param name="html">Html string</param>
        /// <returns>Returns a single 'normal' text</returns>
        private static string RemoveHtmlTags(string html)
        {
            return Regex.Replace(html, @"<\/?([a-z]+?)(\s.*?)*/?>", string.Empty);
        }
    }
}
