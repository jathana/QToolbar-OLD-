using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace QToolbar
{
   public class TreeNodeSerializer<T> where T : IState, new()
   {
      public void Serialize(TreeNode<T> tree, string saveFile)
      {
         Utils.EnsureFolder(Path.GetDirectoryName(saveFile));
         File.WriteAllText(saveFile, tree.SaveState(), Encoding.UTF8);          
      }

      public TreeNode<T> Deserialize(string saveFile)
      {
         TreeNode<T> tree = new TreeNode<T>();

         XmlDocument doc = new XmlDocument();
         doc.Load(saveFile);
         tree.LoadState(doc.DocumentElement);

         return tree; 
      }

   }
}
