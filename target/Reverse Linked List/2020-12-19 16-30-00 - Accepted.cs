/*
Status: Accepted
Runtime: 96 ms
Memory: 25.3 MB
URL: http://leetcode.com/submissions/detail/432374123/
Submission DateTime: December 19, 2020 4:30:00 PM
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
        n.next = p;
        p = n;
        n = nn;
      }
      
      return p;
    }
}
