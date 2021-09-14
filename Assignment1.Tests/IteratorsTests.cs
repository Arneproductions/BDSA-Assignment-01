using Xunit;
using System;
using System.Collections.Generic;

namespace Assignment1.Tests
{
    public class IteratorsTests
    {

        
        
        public static IEnumerable<object[]> FlattenData => new List<object[]>{
            //Arrange
            new object[] {
                new List<List<int>>() {
                    new List<int> {1}, 
                    new List<int> {2}, 
                    new List<int> {3}},

                new List<int> {1, 2, 3}
            },
            new object[] {
                new List<List<int>>() {
                    new List<int> {1, 2, 2, 3}, 
                    new List<int> {3, 3, 4}, 
                    new List<int> {4, 4},
                    new List<int> {4}},

                new List<int> {1, 2, 2, 3, 3, 3, 4, 4, 4, 4}
            }
        };

        [Theory]
        [MemberData(nameof(FlattenData))]
        public void Flatten_streams_to_single_stream(List<List<int>> streams, List<int> expected){

            //Act
            var actual = Iterators.Flatten(streams);

            //Assert
            Assert.Equal(expected, actual);
        }
    



        public static IEnumerable<object[]> FilteredDataEven => new List<object[]>{
            //Arrange
            new object[] {
                new List<int> {1, 2, 3, 4, 5, 6, 7, 8},

                new List<int> {2, 4, 6, 8}
            },
            new object[] {
                new List<int> {0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765},

                new List<int> {0, 2, 8, 34, 144, 610, 2584}
            }
        };

        [Theory]
        [MemberData(nameof(FilteredDataEven))]
        public void Flatten_stream_for_only_even_numbers(List<int> stream, List<int> expected){

            //Act
            var actual = Iterators.Filter(stream, x => x % 2 == 0);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
