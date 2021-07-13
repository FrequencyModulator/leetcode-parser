/*
Status: Accepted
Runtime: 272 ms
Memory: 39.2 MB
URL: http://leetcode.com/submissions/detail/458540863/
Submission DateTime: February 20, 2021 6:01:22 PM
*/
public class Solution {
    public int[] ProductExceptSelf(int[] nums) {
      var products = new int[nums.Length];
      products[0] = 1;
      for(int i = 1; i < nums.Length; i++)
      {
        products[i] = products[i - 1] * nums[i - 1];
      }
      
      int rProduct = 1;
      for(int i = nums.Length - 1; i >= 0 ; i--)
      {
        products[i] = products[i] * rProduct;
        rProduct = rProduct * nums[i];
      }
            
      return products;
    }
}
