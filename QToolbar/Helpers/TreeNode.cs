using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QToolbar
{
   public class TreeNode<T>:IState  where T:IState,new()
   {
      private List<TreeNode<T>> _Children;
      public List<TreeNode<T>> Children {
         get
         {
            return _Children;
         }
      }
      public TreeNode<T> Parent { get; set; }
      public T Data { get; set; }

      public TreeNode()
      {
         Data = new T();
         _Children = new List<TreeNode<T>>();
      }

      public void AddChild(TreeNode<T> child)
      {
         if (Children==null)
         {
            _Children = new List<TreeNode<T>>();
         }
         Children.Add(child);
      }

      public string SaveState()
      {
         var sb = new StringBuilder();
         XmlWriterSettings settings = new XmlWriterSettings() { OmitXmlDeclaration = true };
         using (XmlWriter w = XmlWriter.Create(sb, settings))
         {
            w.WriteStartElement(XmlConvert.EncodeName(GetType().Name));
            w.WriteRaw(((IState)Data).SaveState());
            foreach (TreeNode<T> node in Children)
            {
               w.WriteRaw(((IState)node).SaveState());
            }

            w.WriteEndElement();
            w.Flush();
         }
         return sb.ToString();
      }

      public bool LoadState(XmlNode Node)
      {
         bool retval = true;
         XmlNodeList children = Node.ChildNodes;
         Data = (T)Activator.CreateInstance(typeof(T));
         // take the first child, it is T
         Data.LoadState(Node.FirstChild);
         Children.Clear();
         // exclude first child from recursion, the T the actual information
         for(int i=1;i<children.Count;i++)
         {
            XmlNode child = children[i];
            TreeNode<T> childObj = (TreeNode<T>)Activator.CreateInstance(typeof(TreeNode<T>));
            childObj.Parent = this;
            childObj.LoadState(child);
            Children.Add(childObj);
         }
         return retval;
      }

      public List<T> ToList()
      {
         List<T> ret = new List<T>();
         ToListInternal(this, ret);
         return ret;
      }

      private void ToListInternal(TreeNode<T> node, List<T> result)
      {
         result.Add(node.Data);
         foreach (var child in node.Children)
            ToListInternal(child, result);
      }


   }
}
