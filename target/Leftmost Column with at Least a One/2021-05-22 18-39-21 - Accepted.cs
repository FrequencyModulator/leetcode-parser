/*
Status: Accepted
Runtime: 108 ms
Memory: 27.3 MB
URL: http://leetcode.com/submissions/detail/496843070/
Submission DateTime: May 22, 2021 6:39:21 PM
*/
/**
 * // This is BinaryMatrix's API interface.
 * // You should not implement it, or speculate about its implementation
 * class BinaryMatrix {
 *     public int Get(int row, int col) {}
 *     public IList<int> Dimensions() {}
 * }
 */

class Solution
        {
            public int LeftMostColumnWithOne(BinaryMatrix binaryMatrix)
            {
                int rows = binaryMatrix.Dimensions()[0];
                int cols = binaryMatrix.Dimensions()[1];
                int leftMostCol = -1;

                for (int r = 0; r < rows; r++)
                {                    
                  if (leftMostCol >= 0 && binaryMatrix.Get(r, leftMostCol) == 0) // skip row leftMostCol == 0, becasuse its row leftMostCol is larger then cirrent
                        continue;

                    int rowLeftMostCol = BinSearch(binaryMatrix, r, leftMostCol < 0 ? cols : leftMostCol);

                    if (rowLeftMostCol >= 0)
                    {
                        if (leftMostCol >= 0)
                            leftMostCol = Math.Min(leftMostCol, rowLeftMostCol);
                        else
                            leftMostCol = rowLeftMostCol;
                    }
                }

                return leftMostCol;
            }

            private static int BinSearch(BinaryMatrix binaryMatrix, int row, int length)
            {
                int l = 0;
                int r = length;
                while (l < r)
                {
                    int mid = (r + l) / 2;
                    if (binaryMatrix.Get(row, mid) == 0)                 
                        l = mid + 1;
                    else                    
                        r = mid;
                }                
                return l < 0 || l >= length ? -1 : l;
            }
        }
