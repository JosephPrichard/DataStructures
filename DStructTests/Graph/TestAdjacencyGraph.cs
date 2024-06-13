using DStruct.Graph;
using NUnit.Framework;

namespace DStructTests.Graph;

public class TestAdjacencyGraph
{
    [Test]
    public void Should_Add_Edges()
    {
        var graph = new ListGraph<int>();
        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddVertex(4);
        graph.AddVertex(5);

        graph.AddDirectedEdge(new Edge<int>(1, 2));
        graph.AddDirectedEdge(new Edge<int>(1, 3));
        graph.AddDirectedEdge(new Edge<int>(1, 4));

        graph.AddUndirectedEdge(new Edge<int>(3, 5));
        graph.AddDirectedEdge(new Edge<int>(3, 4));

        var neighbors1 = graph.Neighbors(1);
        var neighbors3 = graph.Neighbors(3);
        var neighbors5 = graph.Neighbors(5);

        var edges = graph.Edges();

        var expected1 = new[] { 2, 3, 4 };
        Assert.That(neighbors1.GetEnumerator(), Is.EqualTo(expected1));

        var expected3 = new[] { 1, 5, 4 };
        Assert.That(neighbors3.GetEnumerator(), Is.EqualTo(expected3));

        var expected5 = new int[] { };
        Assert.That(neighbors5.GetEnumerator(), Is.EqualTo(expected5));

        var expectedEdges = new[]
        {
            new Edge<int>(1, 2), new Edge<int>(1, 3), new Edge<int>(1, 4), new Edge<int>(2, 1),
            new Edge<int>(3, 1), new Edge<int>(3, 5), new Edge<int>(3, 4), new Edge<int>(4, 1), new Edge<int>(4, 3)
        };
        Assert.That(edges.GetEnumerator(), Is.EqualTo(expectedEdges));
    }

    [Test]
    public void Should_Contain_Edges()
    {
        var graph = new ListGraph<int>();
        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);

        graph.AddDirectedEdge(new Edge<int>(1, 2));
        graph.AddDirectedEdge(new Edge<int>(1, 3));

        Assert.That(graph.ContainsEdge(new Edge<int>(1, 2)), Is.EqualTo(true));
        Assert.That(graph.ContainsEdge(new Edge<int>(2, 3)), Is.EqualTo(false));
    }
}