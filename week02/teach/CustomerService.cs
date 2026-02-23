/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {

        // =========================
        // Test 1: Add Customers
        // Scenario: Add customers up to max size and try to exceed it.
        // Expected Result: Should stop adding once max size is reached.
        // =========================
        Console.WriteLine("Test 1");

        var cs = new CustomerService(2);

        cs.AddNewCustomer("Alice", "A1", "Login Issue");
        cs.AddNewCustomer("Bob", "B2", "Password Reset");
        cs.AddNewCustomer("Charlie", "C3", "Billing Question"); // Should not be added

        Console.WriteLine(cs);

        // Defect(s) Found (Original Code):
        // - Queue allowed more than max size because it used > instead of >=

        Console.WriteLine("=================");


        // =========================
        // Test 2: Serve Customers
        // Scenario: Serve customers in FIFO order.
        // Expected Result: Alice served first, then Bob.
        // =========================
        Console.WriteLine("Test 2");

        cs.ServeCustomer();  // Alice
        cs.ServeCustomer();  // Bob
        cs.ServeCustomer();  // Should say no customers

        Console.WriteLine(cs);

        // Defect(s) Found (Original Code):
        // - Customer was removed before being displayed
        // - Wrong customer printed
        // - Exception thrown when only one customer existed

        Console.WriteLine("=================");


        // =========================
        // Test 3: Empty Queue
        // Scenario: Serve from empty queue.
        // Expected Result: Message saying no customers.
        // =========================
        Console.WriteLine("Test 3");

        var cs2 = new CustomerService(3);
        cs2.ServeCustomer();

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId}) : {Problem}";
        }
    }

    /// <summary>
    /// Add a new customer (direct method for testing).
    /// </summary>
    public void AddNewCustomer(string name, string accountId, string problem) {

        // FIX #1: Correct capacity check
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Serve the next customer.
    /// </summary>
    public void ServeCustomer() {

        // FIX #2: Handle empty queue safely
        if (_queue.Count == 0) {
            Console.WriteLine("No customers in queue.");
            return;
        }

        // FIX #3: Display before removing
        var customer = _queue[0];
        Console.WriteLine("Serving: " + customer);
        _queue.RemoveAt(0);
    }

    /// <summary>
    /// Display queue contents
    /// </summary>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " +
               string.Join(", ", _queue) + "]";
    }
}