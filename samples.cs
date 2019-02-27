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
    
    public IList<int> PreorderTraversal(TreeNode root) {
        List<int> r = new List<int>();
        Stack<TreeNode> s = new Stack<TreeNode>();
        s.Push(root);
        while(s.Count > 0)
        {
            TreeNode n = s.Pop();
            
            r.Add(n.val);
            
            if(n.left != null)
            {
                s.Push(n.left);
            }
            
            if(n.right != null)
            {
                s.Push(n.right);
            }
        }
        return r;
    }
}


1. Two Sum
Easy

9876

320

Favorite

Share
Given an array of integers, return indices of the two numbers such that they add up to a specific target.

You may assume that each input would have exactly one solution, and you may not use the same element twice.

Example:

Given nums = [2, 7, 11, 15], target = 9,

Because nums[0] + nums[1] = 2 + 7 = 9,
return [0, 1].
    
  public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        Dictionary<int, List<int>> ind = new Dictionary<int, List<int>>();
        for(int i =0; i < nums.Length; i++)
        {
            if(nums[i] < target)
            {
                int sub = (int)Math.Abs(nums[i] - target);
                if(ind.ContainsKey(nums[i]))
                { 
                    ind[nums[i]].Add(i);
                }
                else
                {
                    ind[nums[i]] = new List<int>{ i };
                }
                   
                if(ind.ContainsKey(sub))
                { 
                    ind[sub].Add(i);
                }
                else
                {
                    ind[sub] = new List<int>{ i };
                }
            }            
        }
                   
        foreach(int num in ind.Keys)
        {
            if(ind[num].Count > 1)
            {
                return ind[num].ToArray();
            }
        }
        
        return null;
    }
}
