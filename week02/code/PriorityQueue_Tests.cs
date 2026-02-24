using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue returns highest priority item
    // Expected Result: Item with highest priority is returned
    // Defect(s) Found:
    // - Last element was not checked due to incorrect loop boundary
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Dequeue removes item from queue
    // Expected Result: Second dequeue returns next highest priority
    // Defect(s) Found:
    // - Item was not removed after dequeue
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);

        var first = priorityQueue.Dequeue();
        var second = priorityQueue.Dequeue();

        Assert.AreEqual("B", first);
        Assert.AreEqual("C", second);
    }

    [TestMethod]
    // Scenario: Dequeue on empty queue
    // Expected Result: Throws InvalidOperationException
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        Assert.ThrowsException<InvalidOperationException>(() =>
        {
            priorityQueue.Dequeue();
        });
    }

    [TestMethod]
    // Scenario: Equal priorities maintain FIFO order
    // Expected Result: First inserted with same priority is removed first
    public void TestPriorityQueue_FIFO_OnEqualPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);

        var first = priorityQueue.Dequeue();
        var second = priorityQueue.Dequeue();

        Assert.AreEqual("First", first);
        Assert.AreEqual("Second", second);
    }
}