// C:\Users\Jorge\Documents\Visual Studio 2010\Projects\C#
//   \LIB\TriesLib\TriesLib\Trie.cs
//
// Library implementing array tries.
//
// Programmer:  Jorge L. Orejel
//
// Last update: 06/09/2021 : Removal of functions {Trie.TreeView} and
//                              {Trie.GenerateTreeView}.
//                           Removal of function {NodeAt}.
//
//      06/07/2021 : Display of message boxes instead of throwing exceptions.
//
//      06/18/2020 : Commenting and polishing of the code.
//
//      12/13/2012 : Original coding.

using System;
using System.Collections.Generic;
//using System.Windows.Forms;

namespace TriesLib
{
   public class Trie
   {
      private TrieNode root; // Root node of the trie.
      private int limit;     // Limit on the possible subtries of a trie node.

      public Trie()
      {
         limit = TLconst.asciiTrieLimit;
         root = new TrieNode( '@', limit, -1, false ); // Root node.
      }// Trie (constructor)

      /// <summary>Insert the characters of a string into the trie.
      /// </summary>
      /// <param name="str">String containing the characters to be inserted.</param>
      /// <param name="_index">Index of {str} in a data set.
      /// </param>
      public void Insert( string str, int _index )
      {
         if ( !string.IsNullOrEmpty( str ) )
         {
            TraverseToInsert( str, _index, root );
         }
         else
         {
            //MessageBox.Show( "Trie class error: null or empty strings cannot be inserted" );
         }
      }// Insert

      /// <summary>Traverse the trie to insert the characters of a non-null, non-empty string.
      /// </summary>
      /// <param name="str">String containing the characters to be inserted.</param>
      /// <param name="_index">Index of {str} in a data set.</param>
      /// <param name="node">Current trie node.
      /// </param>
      private void TraverseToInsert( string str, int _index, TrieNode node )
      {
         int n = str.Length, m = n - 1;

         for ( int i = 0; i < n; ++i )
         {
            char _ch = str[ i ];
            int j = (int)_ch;    // ASCII code of {_ch}.

            // {node[ j ] is {node.subtrie[ j ]} accessed through {node.this[ j ]}

            if (node != null && node[ j ] == null ) 
            {
               node[ j ] = new TrieNode( _ch, limit, _index, i == m );
            }
            if (node != null)
            node = node[ j ];
         }
      }// TraverseToInsert

      /// <summary>Generate a backward listing of the trie indices.
      /// </summary>
      /// <returns>List of the indices.
      /// </returns>
      public List<int> BackwardListingOfEntries()
      {
         List<int> indices = new List<int>();

         DescListNode( root, indices );

         return indices;
      }// BackwardListingOfEntries

      /// <summary>Descending listing of the indices in a trie.
      /// </summary>
      /// <param name="node">Current node in the trie.</param>
      /// <param name="indices">List containing the indices of entries in the trie.
      /// </param>
      private void DescListNode( TrieNode node, List<int> indices )
      {
         if ( node != null )
         {
            if ( node.Index != -1 )
            {
               indices.Add( node.Index );
            }
            for ( int i = limit - 1; i >= 0; --i )
            {
               DescListNode( node[ i ], indices );
            }
         }
      }// DescListNode

      /// <summary>Collect the indices in the trie of all the strings that 
      ///          have the same prefix.
      /// </summary>
      /// <param name="prefix">Prefix for which the indices must be collected.</param>
      /// <returns>List of indices from the trie.
      /// </returns>
      public List<int> Collect( string prefix )
      {
         List<int> indices = new List<int>();

         if ( !string.IsNullOrEmpty( prefix ) )
         {
            int n = prefix.Length;
            TrieNode startNode;

            startNode = TraverseStr( prefix, root );
            if ( startNode != null )
            {
               CollectAll( startNode, indices );
            }
         }
         return indices;
      }// Collect

      /// <summary>Get to the node corresponding to the last character of the prefix of a string.
      /// </summary>
      /// <param name="prefix">String to traverse in the trie.</param>
      /// <param name="startNode">Start node for the traversal of {prefix}.</param>
      /// <returns>Node in the trie corresponding to the last character of {prefix}.
      /// </returns>
      private TrieNode TraverseStr( string prefix, TrieNode startNode )
      {
         TrieNode node = startNode;

         for ( int i = 0; i < prefix.Length; ++i )
         {
            char _ch = prefix[ i ];
            int j = (int)_ch;    // ASCII code of {_ch}

            if ( node[ j ] != null )
            {
               node = node[ j ];
            }
            else
            {
               node = null;
               break;
            }
         }
         return node;
      }// TraverseStr

      /// <summary>Collect the indices of all the nodes under 
      ///          a given node in a trie.
      /// </summary>
      /// <param name="node">Current node in the trie.</param>
      /// <param name="indices">List of all indices collected.
      /// </param>
      private void CollectAll( TrieNode node, List<int> indices )
      {
         if ( node != null )
         {
            if ( node.Index != -1 )
            {
               indices.Add( node.Index );
            }
            for ( int i = 0; i < limit; ++i )
            {
               CollectAll( node[ i ], indices );
            }
         }
      }// CollectAll
   }// Trie (class)
}// TriesLib (namespace)
