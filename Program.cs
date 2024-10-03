using TUnit.Core.Interfaces;

namespace BillioIntegrationTest;

internal class Program
{
    public class LoadTestParallelLimit : IParallelLimit
    {
        public int Limit => 50;
    }
    /*
After 1.1
2-Sellers
 1 Add (3 to different users)
 2 Get (3)
 2 GetAll
 2 Get with query params
 3 Delete (Add and delete 1)
 3 Update (3)
    */
    /*
After 2.1
3-Customer
 1 Add (3 to different sellers)
 2 Get (3)
 2 GetAll
 2 GetAll with query params
 3 Delete (Add and delete 1)
 3 Update (3)
    */
    /*
After 3.1
4-item
 1 Add (3 to different customers)
 2 Get (3)
 2 GetAll
 2 GetAll with query params
 3 Delete (Add and delete 1)
 3 Update (3)
    */
    /*
After 4.1
5-Invoice
 1 Add (3 to different customers and sellers) dont do parrallel
 2 Get (3)
 2 GetAll
 2 GetAll with query params
 2 GeneratePDF (3)
 3 Delete (Add and delete 1)
 3 Update (3)
  */
}
