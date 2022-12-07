global using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary1;

namespace Test;

public class DynamicArrayTest
{
    public class IntsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var i in Enumerable.Range(0, 10))
                yield return new object[] {i};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    [Theory]
    [ClassData(typeof(IntsTestData))]
    public void Count_ReturnsCorrectValue(int supposedLength)
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, supposedLength))
            dynamicArray.AddLast(i);

        // Act
        var count = dynamicArray.Count;

        // Assert
        Assert.Equal(supposedLength, count);
    }
    
    [Fact]
    public void AddFirst_AddsFirstElement()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        // Act
        dynamicArray.AddFirst(1);

        // Assert
        var array = dynamicArray.ToArray();
        Assert.Contains(1, array);
    }
    
    [Fact]
    public void AddFirst_AddsFirstElementOnArray()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();
        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);
    
        var list = dynamicArray.ToList();
        list.Insert(0, 10);
        
        // Act
        dynamicArray.AddFirst(10);
    
        // Assert
        var array = list.ToArray();
        var array2 = dynamicArray.ToArray();
        Assert.Equal(array , array2 );
    }
    
    [Fact]
    public void AddLast_AddsLastElement()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        // Act
        dynamicArray.AddLast(1);

        // Assert
        var array = dynamicArray.ToArray();
        Assert.Contains(1, array);
    }
    
    [Fact]
    public void AddLast_AddsLastElementOnArray()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();
        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddFirst(i);
    
        var list = dynamicArray.ToList();
        list.Add(10);
        
        // Act
        dynamicArray.AddLast(10);
    
        // Assert
        var array = list.ToArray();
        var array2 = dynamicArray.ToArray();
        Assert.Equal(array , array2 );
    }
    
    
    
    [Fact]
    public void Clear_ResetsState()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);

        // Act
        dynamicArray.Clear();

        // Assert
        var array = dynamicArray.ToArray();
        Assert.Empty(array);
    }
    
    [Fact]
    public void RemoveFirst_RemovesFirstElement()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);

        // Act
        var result = dynamicArray.RemoveFirst();

        //Assert
        var array = dynamicArray.ToArray();

        Assert.DoesNotContain(0, array);
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void RemoveFirst_RemovesFirstElementInEmptyList()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        // Act & Assert

        Assert.Throws<InvalidOperationException>(() => dynamicArray.RemoveFirst());
    }

    [Fact]
    public void RemoveLast_RemovesLastElement()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);

        // Act
        var result = dynamicArray.RemoveLast();

        //Assert
        var array = dynamicArray.ToArray();

        Assert.DoesNotContain(9, array);
        Assert.Equal(9, result);
    }
    
    [Fact]
    public void RemoveLast_RemovesLastElementInEmptyList()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        // Act & Assert

        Assert.Throws<InvalidOperationException>(() => dynamicArray.RemoveLast());
    }
    
    [Fact]
    public void First_ReturnFirstElement()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);

        // Act
        var result = dynamicArray.First();

        //Assert
        
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void First_ReturnFirstElementFromEmptyList()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        // Act & Assert

        Assert.Throws<InvalidOperationException>(() => dynamicArray.First());
    }
    
    [Fact]
    public void Last_ReturnLastElement()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);

        // Act
        var result = dynamicArray.Last();

        //Assert
        
        Assert.Equal(9, result);
    }
    
    [Fact]
    public void Last_ReturnLastElementFromEmptyList()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        // Act & Assert

        Assert.Throws<InvalidOperationException>(() => dynamicArray.Last());
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(5, true)]
    [InlineData(10, false)]
    public void Contains(int elem, bool expectedResult)
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);

        // Act
        var result = dynamicArray.Contains(elem);

        // Assert
        Assert.Equal(expectedResult, result);
    }
    
    [Fact]
    public void CopyTo_ThrowsOnArgumentNull()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        // Act
        var act = new Action(() => { dynamicArray.CopyTo(null, 0); });

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void CopyTo_ThrowsOnArrayIndexLessThanZero()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        // Act
        var act = new Action(() =>
        {
            var array = new int[1];
            dynamicArray.CopyTo(array, -1);
        });

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(act);
    }

    [Fact]
    public void CopyTo_ThrowsOnArgumentException()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        // Act
        var act = new Action(() =>
        {
            var array = new int[1];
            dynamicArray.CopyTo(array, 2);
        });

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void CopyTo_CopiesFromStart()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);

        var array = new int[dynamicArray.Count];

        // Act
        dynamicArray.CopyTo(array, 0);

        // Assert
        Assert.True(dynamicArray.SequenceEqual(array));
    }

    [Theory]
    [ClassData(typeof(IntsTestData))]
    public void CopyTo_CopiesWithArrayIndex(int arrayIndex)
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);

        var array = new int[dynamicArray.Count + arrayIndex];

        // Act
        dynamicArray.CopyTo(array, arrayIndex);

        // Assert
        var list = Enumerable.Range(0, 10).ToList();

        var arrayFromList = new int[list.Count + arrayIndex];
        list.CopyTo(arrayFromList, arrayIndex);

        Assert.True(array.SequenceEqual(arrayFromList));
    }

    [Fact]
    public void GetEnumerator_EnumeratesProperly()
    {
        // Arrange
        var dynamicArray = new DynamicArray<int>();

        foreach (var i in Enumerable.Range(0, 10))
            dynamicArray.AddLast(i);

        // Act
        var items = dynamicArray.Select(i => i);

        // Assert
        Assert.Equal(Enumerable.Range(0, 10), items);
    }
}