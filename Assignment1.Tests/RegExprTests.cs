using System.Collections.Generic;
using Xunit;

namespace Assignment1.Tests
{
    public class RegExprTests
    {
        [Fact]
        public void SplitLine_LinesWithSpace_ReturnWords()
        {
            IEnumerable<string> lines = new string[] {"Hej med dig", 
                                                    "ABC     abc 123   jj  123", 
                                                    "DE danskemestre BIF 2021"};

            foreach(string word in RegExpr.SplitLine(lines))
            {
                Assert.NotEmpty(word);
                Assert.NotNull(word);
                Assert.False(word.Contains(" "));
            }
        }

        [Fact]
        public void Resolution_LineWidthCommaSeperatedResolutions_ReturnCollectionOfTuples()
        {
            // Arrange
            string resolution = "1920x1080, 1010x15000 11132131x23231x21231, abcdjfskx12331";
            var expectedResult = new []{ (1920, 1080), (1010, 15000) };

            // Act
            var result = RegExpr.Resolution(resolution);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void InnerText_ATags_ReturnCollectionWithTextFromATags()
        {
            // Arrange
            string html = @"<div><p>A <b>regular expression</b>, <b dumtAttr='rigtigdufjdskafl'>regex</b> or <b>regexp</b> (sometimes called a <b>rational expression</b>) is, in <a href='/wiki/Theoretical_computer_science' title='Theoretical computer science'>theoretical computer science</a> and <a href='/wiki/Formal_language' title='Formal language'>formal language</a> theory, a sequence of <a href='/wiki/Character_(computing)' title='Character (computing)'>characters</a> that define a <i>search <a href='/wiki/Pattern_matching' title='Pattern matching'>pattern</a></i>. Usually this pattern is then used by <a href='/wiki/String_searching_algorithm' title='String searching algorithm'>string searching algorithms</a> for 'find' or 'find and replace' operations on <a href='/wiki/String_(computer_science)' title='String (computer science)'>strings</a>.</p></div>";
            var expectedResult = new []{"theoretical computer science", "formal language", "characters", "pattern", "string searching algorithms", "strings"};
            
            // Act
            var result = RegExpr.InnerText(html, "a");

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void InnerText_PTags_ReturnCollectionWithOnlyText()
        {
            string html = @"<div><p>The phrase <a/><i>regular expressions</i> (and consequently, regexes) is often used to mean the specific, standard textual syntax for representing <u>patterns</u> that matching <em>text</em> need to conform to.</p></div>";
            var expectedResult = new []{"The phrase regular expressions (and consequently, regexes) is often used to mean the specific, standard textual syntax for representing patterns that matching text need to conform to."};

            var result = RegExpr.InnerText(html, "p");

            Assert.Equal(expectedResult, result);
        }
    }
}
