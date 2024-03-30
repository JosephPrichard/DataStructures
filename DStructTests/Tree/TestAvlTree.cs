using DStruct.Tree.avl;
using NUnit.Framework;

namespace DStructTests.Tree
{
    public class TestAvlTree
    {
        [Test]
        public void Should_Put_Then_Order_Iterate()
        {
            var tree = new AvlTree<int, char>();
            tree.Put(20, 'A');
            tree.Put(4, 'B');
            tree.Put(26, 'C');
            tree.Put(3, 'D');
            tree.Put(9, 'E');
            tree.Put(2, 'F');
            tree.Put(7, 'G');
            tree.Put(11, 'H');
            tree.Put(21, 'I');

            var expected = new[] {'F', 'D', 'B', 'G', 'E', 'H', 'A', 'I', 'C'};
            Assert.That(tree.Elements(), Is.EqualTo(expected));
        }
        
        [Test]
        public void Should_Put_Then_Remove()
        {
            var tree = new AvlTree<int, char>();
            tree.Put(20, 'A');
            tree.Put(4, 'B');
            tree.Put(26, 'C');
            tree.Put(3, 'D');
            tree.Put(9, 'E');
            tree.Put(2, 'F');
            tree.Put(7, 'G');
            tree.Put(11, 'H');
            tree.Put(21, 'I');
            
            tree.Remove(2);
            tree.Put(31, 'Z');

            Assert.That(tree.Contains(2), Is.EqualTo(false));
            
            tree.Remove(3);
            tree.Remove(4);
            tree.Remove(7);
            tree.Remove(11);
            
            Assert.That(tree.Contains(3), Is.EqualTo(false));
            Assert.That(tree.Contains(4), Is.EqualTo(false));
            Assert.That(tree.Contains(7), Is.EqualTo(false));
            Assert.That(tree.Contains(11), Is.EqualTo(false));
            
            tree.Put(30, 'K');
            
            tree.Remove(31);
            tree.Remove(30);
            tree.Remove(26);
            tree.Remove(9);
            tree.Remove(21);
            tree.Remove(20);
            
            Assert.That(tree.Contains(31), Is.EqualTo(false));
            Assert.That(tree.Contains(30), Is.EqualTo(false));
            Assert.That(tree.Contains(26), Is.EqualTo(false));
            Assert.That(tree.Contains(9), Is.EqualTo(false));
            Assert.That(tree.Contains(21), Is.EqualTo(false));
            Assert.That(tree.Contains(20), Is.EqualTo(false));
        }
        
        [Test]
        public void Should_Put_Then_Range_Iterate()
        {
            var tree = new AvlTree<int, char>();
            tree.Put(20, 'A');
            tree.Put(4, 'B');
            tree.Put(26, 'C');
            tree.Put(3, 'D');
            tree.Put(9, 'E');
            tree.Put(2, 'F');
            tree.Put(7, 'G');
            tree.Put(11, 'H');
            tree.Put(21, 'I');
            tree.Put(28, 'J');

            var expected = new[] {'E', 'H', 'A', 'I'};
            Assert.That(tree.RangeSearch(8, 22), Is.EqualTo(expected));
        }
        
        [Test]
        public void Should_Put_Then_Remove_Then_RangeSearch()
        {
            var tree = new AvlTree<int, char>();
            tree.Put(20, 'A');
            tree.Put(4, 'B');
            tree.Put(26, 'C');
            tree.Put(3, 'D');
            tree.Put(9, 'E');
            
            tree.Remove(3);
            tree.Remove(9);
            
            tree.Put(2, 'F');
            tree.Put(7, 'G');
            tree.Put(36, 'Z');
            tree.Put(11, 'H');
            tree.Put(21, 'I');
            
            tree.Remove(7);
            tree.Put(31, 'M');
            
            var expected = new[] {'B', 'H', 'A', 'I', 'C'};
            Assert.That(tree.RangeSearch(4, 26), Is.EqualTo(expected));
        }
    }
}