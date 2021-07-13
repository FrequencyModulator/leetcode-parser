/*
Status: Accepted
Runtime: 96 ms
Memory: 24.9 MB
URL: http://leetcode.com/submissions/detail/432373622/
Submission DateTime: December 19, 2020 4:27:51 PM
*/
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode ReverseList(ListNode head) {
      ListNode p = null;
      ListNode n = head;
      
      while(n != null)
      {
        var nn = n.next;
        var pp = n;
        n.next = p;
        p = pp;
        n = nn;
      }
      
      return p;
    }
}
