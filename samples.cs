/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution {
    
    private void f(IList<int> r, TreeNode root)
    {
        r.Add(root.val);
        if(root.left != null)
        {
            f(r, root.left);
        }
        
        if(root.right != null)
        {
            f(r, root.right);
        }
    }
    
    public IList<int> PreorderTraversal(TreeNode root) {
        List<int> r = new List<int>();
        f(r, root);
        
        return r;
    }
}

