using DStruct.stack;
using NUnit.Framework;

namespace DStructTests.list
{
    public class TestStack
    {
        [Test]
        public void Should_Push()
        {
            var stack = new Stack<int>();
            stack.PushFront(6);
            stack.PushFront(5);
            stack.PushFront(4);

            var expected = new[] {4, 5, 6};
            Assert.That(stack.GetEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void Should_Pop()
        {
            var stack = new Stack<int>();
            stack.PushFront(6);
            stack.PushFront(5);
            stack.PushFront(4);

            var expected = new[] {4, 5, 6};
            foreach (var e in expected)
            {
                var v = stack.Pop();
                Assert.That(v, Is.EqualTo(e));
            }
        }

        [Test]
        public void Should_AddAll()
        {
            var stack = new Stack<int>();
            stack.PushFront(6);
            stack.PushFront(5);
            stack.PushFront(4);
            
            var stack1 = new Stack<int>();
            stack1.PushFront(3);
            stack1.PushFront(2);
            stack1.PushFront(1);

            stack1.AddAll(stack1);
            stack.AddAll(stack1);
            
            var expected = new[] {1, 2, 3, 1, 2, 3, 4, 5, 6};
            Assert.That(stack.GetEnumerable(), Is.EqualTo(expected));
        }
    }
}