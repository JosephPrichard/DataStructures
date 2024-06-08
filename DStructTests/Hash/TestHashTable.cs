using DStruct.Hash;
using NUnit.Framework;

namespace DStructTests.Hash
{
    public class TestHashTable
    {
        [Test]
        public void Should_Put_Then_Set()
        {
            var table = new HashTable<int, char>(25);
            table.Put(1, 'A');
            table.Put(2, 'B');
            table.Put(3, 'C');
            table.Put(51, 'D');
            table.Put(52, 'E');
            table.Put(53, 'F');
            table.Put(101, 'G');
            table.Put(102, 'H');
            table.Put(103, 'I');
            
            Assert.That(table.Size, Is.EqualTo(9));
            
            Assert.That(table.Get(3), Is.EqualTo('C'));
            Assert.That(table.Get(53), Is.EqualTo('F'));
            Assert.That(table.Get(103), Is.EqualTo('I'));
            Assert.That(table.Get(1), Is.EqualTo('A'));
            Assert.That(table.Get(51), Is.EqualTo('D'));
            Assert.That(table.Get(101), Is.EqualTo('G'));
            Assert.That(table.Get(0), Is.EqualTo('\0'));
        }

        [Test]
        public void Should_Remove_Then_Contains_False()
        {
            var table = new HashTable<int, char>(25);
            table.Put(1, 'A');
            table.Put(2, 'B');
            table.Put(3, 'C');
            table.Put(51, 'D');
            table.Put(52, 'E');
            table.Put(53, 'F');
            table.Put(101, 'G');
            table.Put(102, 'H');
            table.Put(103, 'I');

            Assert.That(table.Contains(3), Is.EqualTo(true));
            table.Remove(3);
            Assert.That(table.Contains(3), Is.EqualTo(false));
            
            Assert.That(table.Contains(53), Is.EqualTo(true));
            Assert.That(table.Contains(103), Is.EqualTo(true));
            
            table.Remove(53);
            table.Remove(103);
            
            Assert.That(table.Contains(53), Is.EqualTo(false));
            Assert.That(table.Contains(103), Is.EqualTo(false));
            
            Assert.That(table.Size, Is.EqualTo(6));
        }
        
        [Test]
        public void Should_Clear_Then_Contains_False()
        {
            var table = new HashTable<int, char>(25);
            table.Put(1, 'A');
            table.Put(2, 'B');
            table.Put(3, 'C');
            table.Put(51, 'D');
            table.Put(52, 'E');
            table.Put(53, 'F');
            table.Put(101, 'G');
            table.Put(102, 'H');
            table.Put(103, 'I');
            
            Assert.That(table.Contains(53), Is.EqualTo(true));
            Assert.That(table.Contains(103), Is.EqualTo(true));

            table.Clear();
            
            Assert.That(table.Contains(53), Is.EqualTo(false));
            Assert.That(table.Contains(103), Is.EqualTo(false));
            
            Assert.That(table.Size, Is.EqualTo(0));
        }
    }
}