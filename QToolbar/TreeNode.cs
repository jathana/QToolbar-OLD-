using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar
{
   public class TreeNode<T> where T:new()
   {
      public List<TreeNode<T>> Children { get; set; }
      public TreeNode<T> Parent { get; set; }
      public T Data { get; set; }

      public TreeNode()
      {
         Data = new T();
         Children = new List<TreeNode<T>>();
      }

      public void AddChild(TreeNode<T> child)
      {
         if (Children==null)
         {
            Children = new List<TreeNode<T>>();
         }
         Children.Add(child);
      }

   }
}
