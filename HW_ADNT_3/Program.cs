using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HW_ADNT_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connect = "Data Source=DESKTOP-V5OB79V;Initial Catalog=StatiShop;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlCommand cmd = null;

                conn.Open();

                string choise = "";
                try
                {
                    while (true)
                    {
                        Console.WriteLine("Choose:\n1 - Add product\n2 - Add type\n3 - Add manager\n4 - Add firm" +
                            "\n5 - Update product\n6 - Update type\n7 - Update manager\n8 - Update Firm" +
                            "\n9 - Delete product\n10 - Delete type\n11 - Delete manager\n12 - Delete firm" +
                            "\n13 - Manager with most sales\n14 - Manager with most profit\n15 - Manager with most profit in custom time linet" +
                            "\n16 - Firm what bought by biggest sum\n17 - Most sold tupe\n18 - Most profit type\n19 - Names of most popular type" +
                            "\n20 - Product not sold [n] days");
                        choise = Console.ReadLine();
                        if (choise == "0")
                        {
                            break;
                        }
                        else if (choise == "1")
                        {
                            Console.WriteLine("Enter name:\n");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter type ID:\n");
                            string typeID = Console.ReadLine();
                            Console.WriteLine("Enter price:\n");
                            string price = Console.ReadLine();
                            cmd = new SqlCommand("INSERT INTO Product (Name, TypeID, Price) VALUES (@Name, @TypeID, @Price)", conn);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@TypeID", typeID);
                            cmd.Parameters.AddWithValue("@Price", price);
                        }
                        else if (choise == "2")
                        {
                            Console.WriteLine("Enter name:\n");
                            string name = Console.ReadLine();
                            cmd = new SqlCommand("INSERT INTO Type (Name) VALUES (@Name)", conn);
                            cmd.Parameters.AddWithValue("@Name", name);
                        }
                        else if (choise == "3")
                        {
                            Console.WriteLine("Enter name:\n");
                            string name = Console.ReadLine();
                            cmd = new SqlCommand("INSERT INTO Manager (Name) VALUES (@Name)", conn);
                            cmd.Parameters.AddWithValue("@Name", name);
                        }
                        else if (choise == "4")
                        {
                            Console.WriteLine("Enter name:\n");
                            string name = Console.ReadLine();
                            cmd = new SqlCommand("INSERT INTO Firm (Name) VALUES (@Name)", conn);
                            cmd.Parameters.AddWithValue("@Name", name);
                        }
                        else if (choise == "5")
                        {
                            Console.WriteLine("Enter ID of product you want to update:\n");
                            string ID = Console.ReadLine();
                            Console.WriteLine("Enter name of collumn you want update(Name, TypeID, Price):\n");
                            string col = Console.ReadLine();
                            Console.WriteLine("Enter value of collumn(Name - string, TypeID - int, Price - int):\n");
                            string value = Console.ReadLine();
                            cmd = new SqlCommand($"UPDATE Product SET {col} = @{col} WHERE ID = @ID", conn);
                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue($"@{col}", value);
                        }
                        else if (choise == "6")
                        {
                            Console.WriteLine("Enter ID of type you want to update:\n");
                            string ID = Console.ReadLine();
                            Console.WriteLine("Enter name of collumn you want update(Name):\n");
                            string col = Console.ReadLine();
                            Console.WriteLine("Enter value of collumn(Name - string):\n");
                            string value = Console.ReadLine();
                            cmd = new SqlCommand($"UPDATE Type SET {col} = @{col} WHERE ID = @ID", conn);
                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue($"@{col}", value);
                        }
                        else if (choise == "7")
                        {
                            Console.WriteLine("Enter ID of manager you want to update:\n");
                            string ID = Console.ReadLine();
                            Console.WriteLine("Enter name of collumn you want update(Name):\n");
                            string col = Console.ReadLine();
                            Console.WriteLine("Enter value of collumn(Name - string):\n");
                            string value = Console.ReadLine();
                            cmd = new SqlCommand($"UPDATE Manager SET {col} = @{col} WHERE ID = @ID", conn);
                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue($"@{col}", value);
                        }
                        else if (choise == "8")
                        {
                            Console.WriteLine("Enter ID of Firm you want to update:\n");
                            string ID = Console.ReadLine();
                            Console.WriteLine("Enter name of collumn you want update(Name):\n");
                            string col = Console.ReadLine();
                            Console.WriteLine("Enter value of collumn(Name - string):\n");
                            string value = Console.ReadLine();
                            cmd = new SqlCommand($"UPDATE Firm SET {col} = @{col} WHERE ID = @ID", conn);
                            cmd.Parameters.AddWithValue("@ID", ID);
                            cmd.Parameters.AddWithValue($"@{col}", value);
                        }
                        else if (choise == "9")
                        {
                            Console.WriteLine("Enter ID of product you want to delete:\n");
                            string ID = Console.ReadLine();
                            cmd = new SqlCommand("DELETE FROM Product WHERE ID = @ID", conn);
                            cmd.Parameters.AddWithValue("@ID", ID);
                        }
                        else if (choise == "10")
                        {
                            Console.WriteLine("Enter ID of type you want to delete:\n");
                            string ID = Console.ReadLine();
                            cmd = new SqlCommand("DELETE FROM Type WHERE ID = @ID", conn);
                            cmd.Parameters.AddWithValue("@ID", ID);
                        }
                        else if (choise == "11")
                        {
                            Console.WriteLine("Enter ID of Manager you want to delete:\n");
                            string ID = Console.ReadLine();
                            cmd = new SqlCommand("DELETE FROM Manager WHERE ID = @ID", conn);
                            cmd.Parameters.AddWithValue("@ID", ID);
                        }
                        else if (choise == "12")
                        {
                            Console.WriteLine("Enter ID of Firm you want to delete:\n");
                            string ID = Console.ReadLine();
                            cmd = new SqlCommand("DELETE FROM Firm WHERE ID = @ID", conn);
                            cmd.Parameters.AddWithValue("@ID", ID);
                        }
                        else if (choise == "13")
                        {
                            //О-ох, поехали, сайт у меня с синтаксисом SQL открыт, сейчас будем вспоминать...
                            cmd = new SqlCommand("SELECT TOP 1 SUM(Sales.Amount) AS s, Manager.Name FROM Sales " +
                                "LEFT JOIN Manager ON Sales.Manager_ID = Manager.ID " +
                                "GROUP BY Manager.Name ORDER BY s DESC", conn);
                            //Ладно, заняло минут 5 что б заставить работать
                        }
                        else if (choise == "14")
                        {
                            cmd = new SqlCommand("SELECT TOP 1 SUM(Sales.Amount * Product.Price) AS s, Manager.Name FROM Sales " +
                                "LEFT JOIN Manager ON Sales.Manager_ID = Manager.ID " +
                                "LEFT JOIN Product ON Sales.Product_ID = Product.ID " +
                                "GROUP BY Manager.Name ORDER BY s DESC", conn);
                        }
                        else if (choise == "15")
                        {
                            Console.WriteLine("Enter upper limit of date(YYYY-MM-DD):\n");
                            string upper = Console.ReadLine();
                            Console.WriteLine("Enter lower limit of date(YYYY-MM-DD):\n");
                            string lower = Console.ReadLine();
                            cmd = new SqlCommand("SELECT TOP 1 SUM(Sales.Amount * Product.Price) AS s, Manager.Name FROM Sales " +
                                "LEFT JOIN Manager ON Sales.Manager_ID = Manager.ID " +
                                "LEFT JOIN Product ON Sales.Product_ID = Product.ID " +
                                $"WHERE Sales.Date < '{upper}' AND Sales.Date > '{lower}' " +
                                "GROUP BY Manager.Name ORDER BY s DESC", conn);
                        }
                        else if (choise == "16")
                        {
                            cmd = new SqlCommand("SELECT TOP 1 SUM(Sales.Amount * Product.Price) AS s, Firm.Name FROM Sales " +
                                "LEFT JOIN Firm ON Sales.Firm_ID = Firm.ID " +
                                "LEFT JOIN Product ON Sales.Product_ID = Product.ID " +
                                "GROUP BY Firm.Name ORDER BY s DESC", conn);
                        }
                        else if (choise == "17")
                        {
                            cmd = new SqlCommand("SELECT TOP 1 SUM(Sales.Amount) AS s, Type.Name FROM Sales " +
                                "LEFT JOIN Product ON Sales.Product_ID = Product.ID " +
                                "LEFT JOIN Type ON Product.TypeID = Type.ID " +
                                "GROUP BY Type.Name ORDER BY s DESC", conn);
                        }
                        else if (choise == "18")
                        {
                            cmd = new SqlCommand("SELECT TOP 1 SUM(Sales.Amount * Product.Price) AS s, Type.Name FROM Sales " +
                                "LEFT JOIN Product ON Sales.Product_ID = Product.ID " +
                                "LEFT JOIN Type ON Product.TypeID = Type.ID " +
                                "GROUP BY Type.Name ORDER BY s DESC", conn);
                        }
                        else if (choise == "19")
                        {
                            cmd = new SqlCommand("SELECT SUM(Sales.Amount) AS s, Product.Name FROM Sales " +
                                "RIGHT JOIN Product ON Sales.Product_ID = Product.ID " +
                                "GROUP BY Product.Name ORDER BY s DESC", conn);
                        }
                        else if (choise == "20")
                        {
                            Console.WriteLine("Enter amount of days:\n");
                            int days = Convert.ToInt32(Console.ReadLine());
                            DateTime date = DateTime.Now;
                            date = date.AddDays(-days);
                            cmd = new SqlCommand("SELECT MAX(Sales.Date) AS s, Product.Name FROM Sales " +
                                "RIGHT JOIN Product ON Sales.Product_ID = Product.ID " +
                                $"WHERE Sales.Date < '{date.Year}-{date.Month}-{date.Day}'" +
                                "GGROUP BY Product.Name ORDER BY s ASC", conn);
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Invalid option!");
                        }
                        using (cmd)
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write(reader[i] + " ");
                                    }
                                    Console.WriteLine('\n');
                                }
                            }
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex}");
                }
            }
        }
    }
}
