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


Two Sum
Given nums = [2, 7, 11, 15], target = 9,

Because nums[0] + nums[1] = 2 + 7 = 9,
return [0, 1].
    
 public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        if(nums == null)
        {
            return null;
        }
        Dictionary<int, HashSet<int>> ind = new Dictionary<int, HashSet<int>>();
        for(int i =0; i < nums.Length; i++)
        {
            int a = Math.Abs(nums[i]);
            int sub = (int)Math.Abs(nums[i] -target);
                if(ind.ContainsKey(a))
                { 
                    if(!ind[a].Contains(i) && nums[ind[a].First()] + nums[i] == target)
                    {
                        ind[a].Add(i);
                    }
                }
                else
                {
                    ind[a] = new HashSet<int>{ i };
                }
                   
                if(ind.ContainsKey(sub))
                { 
                    if(!ind[sub].Contains(i) && nums[ind[sub].First()] + nums[i] == target)
                    {
                        ind[sub].Add(i);
                    }
                }
                else
                {
                    ind[sub] = new HashSet<int>{ i };
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


public class Solution
{
    public int[] cellCompete(int[] states, int days)
    {
     int[] newStates = new int[states.Length];
     for(int i = 0; i < days; i++)
     {
         for(int j = 0; j < states.Length; j++)
         {
             int left = 0;
             int right = 0;
     
             if(j >0)
             {
                 left = states[j-1];
             }
             
             if(j <states.Length -1)
             {
                 right = states[j+1];
             }
             
             newStates[j] = left ^ right;
         }
         
        System.Array.Copy(newStates, 0, states, 0, states.Length);
     }
     
     return states;
    }
    
}
