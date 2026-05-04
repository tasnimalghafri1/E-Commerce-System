using System;
using System.Collections.Generic;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int Quantity;
}

class Program
{
    static Dictionary<int, Product> products = new Dictionary<int, Product>();
    static List<Product> cart = new List<Product>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n1. Add Product");
            Console.WriteLine("2. View Products");
            Console.WriteLine("3. Search Product");
            Console.WriteLine("4. Add to Cart");
            Console.WriteLine("5. View Cart");
            Console.WriteLine("6. Checkout");
            Console.WriteLine("7. Exit");

            Console.Write(" please Choose: ");

            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: AddProduct(); break;
                    case 2: ViewProducts(); break;
                    case 3: SearchMenu(); break;
                    case 4: AddToCartMenu(); break;
                    case 5: ViewCart(); break;
                    case 6: Checkout(); break;
                    case 7: return;
                    default: Console.WriteLine("Invalid choice"); break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input!");
            }
        }
    }

    // Add Product
    static void AddProduct()
    {
        try
        {
            Product p = new Product();

            Console.Write("Id: ");
            p.Id = Convert.ToInt32(Console.ReadLine());

            if (products.ContainsKey(p.Id))
            {
                Console.WriteLine("Product already exists!");
                return;
            }

            Console.Write("Name: ");
            p.Name = Console.ReadLine();

            Console.Write("Price: ");
            p.Price = double.Parse(Console.ReadLine());

            Console.Write("Quantity: ");
            p.Quantity = Convert.ToInt32(Console.ReadLine());

            products[p.Id] = p;

            Console.WriteLine($"Added: {p.Name}");
        }
        catch
        {
            Console.WriteLine("Invalid input!");
        }
    }

    // View Products
    static void ViewProducts()
    {
        foreach (var p in products.Values)
        {
            Console.WriteLine($"ID:{p.Id} Name:{p.Name} Price:{p.Price} Qty:{p.Quantity}");
        }
    }

    // Search Menu
    static void SearchMenu()
    {
        Console.Write("Search by (1=Id, 2=Name): ");
        int c = Convert.ToInt32(Console.ReadLine());

        if (c == 1)
        {
            Console.Write("Enter Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SearchProduct(id);
        }
        else
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            SearchProduct(name);
        }
    }

    // Overloading
    static void SearchProduct(int id)
    {
        if (FindProduct(id, out Product p))
            Console.WriteLine($"Found: {p.Name} - {p.Price}");
        else
            Console.WriteLine("Product not found");
    }

    static void SearchProduct(string name)
    {
        foreach (var p in products.Values)
        {
            if (p.Name.ToLower() == name.ToLower())
            {
                Console.WriteLine($"Found: {p.Name} - {p.Price}");
                return;
            }
        }
        Console.WriteLine("Product not found");
    }

    // out
    static bool FindProduct(int id, out Product found)
    {
        return products.TryGetValue(id, out found);
    }

    // Add to Cart
    static void AddToCartMenu()
    {
        try
        {
            Console.Write("Product Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Quantity: ");
            int qty = Convert.ToInt32(Console.ReadLine());

            AddToCart(id, qty);
        }
        catch
        {
            Console.WriteLine("Invalid input!");
        }
    }

    static void AddToCart(int productId, int quantity)
    {
        if (!FindProduct(productId, out Product p))
        {
            Console.WriteLine("Product not found");
            return;
        }

        if (p.Quantity < quantity)
        {
            Console.WriteLine("Not enough quantity");
            return;
        }

        UpdateQuantity(ref p.Quantity, quantity);

        cart.Add(new Product
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quantity = quantity
        });

        Console.WriteLine($"Added to cart: {p.Name}");
    }

    // ref
    static void UpdateQuantity(ref int stock, int qty)
    {
        stock -= qty;
    }

    // Recursion
    static void ViewCart()
    {
        Console.WriteLine("Cart:");
        DisplayCartRecursive(0);
    }

    static void DisplayCartRecursive(int index)
    {
        if (index >= cart.Count) return;

        var item = cart[index];
        Console.WriteLine($"{item.Name} x{item.Quantity}");

        DisplayCartRecursive(index + 1);
    }

    // Checkout
    static void Checkout()
    {
        double total = 0;

        foreach (var item in cart)
        {
            total += item.Price * item.Quantity;
        }

        Console.WriteLine($"Total: {total}");
        cart.Clear();
    }
}