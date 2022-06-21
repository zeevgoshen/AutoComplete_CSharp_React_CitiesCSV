// C:\Users\Jorge\Documents\Visual Studio 2010\Projects\C#
//   \LIB\TriesLib\TriesLib\TrieNode.cs
//
// Library implementing array tries.
//
// Programmer:  Jorge L. Orejel
//
// Last update: 06/07/2021 : Display of message boxes instead of throwing exceptions.
//
//      06/18/2020 : Commenting and polishing of the code.
//
//      12/13/2012 : Original coding.

using System;
//using System.Windows.Forms;

namespace TriesLib
{
   public class TrieNode
   {
      private char ch;            // Character at some position in a string.
      private int limit, index;   // Number of subtrie nodes and index of the trie node.
      private TrieNode[] subtrie; // Array of all possible trie nodes that can follow 'ch'.

      /// <summary>Create an instance of a TrieNode with the given parameters.
      /// </summary>
      /// <param name="_ch">Character in the trie node.</param>
      /// <param name="_limit">Number of subtrie nodes.</param>
      /// <param name="_index">Index of the trie node.</param>
      /// <param name="endOfString">Whether or not the end of a string has been reached.
      /// </param>
      public TrieNode( char _ch, int _limit, int _index, bool endOfString )
      {
         ch = _ch;
         limit = _limit;

         subtrie = new TrieNode[ limit ];

         for ( int i = 0; i < limit; ++i )
         {
            subtrie[ i ] = null;
         }
         index = endOfString ? _index : -1;
      }// TrieNode (constructor)

      // Read-only properties.

      public char Ch { get { return ch; } }

      public int Limit { get { return limit; } }

      public int Index { get { return index; } }

      /// <summary>Get or Set the i-th subtrie of a trie node.
      /// </summary>
      /// <param name="i">Index of the subtrie.</param>
      /// <returns>If the index {i} is valid, the i-th subtrie of a trie node.
      /// </returns>
      public TrieNode this[ int i ]
      {
         get
         {
            if ( 0 <= i && i < limit )
            {
               return subtrie[ i ];
            }
            else
            {
               InvalidIndex( i );
               return null;
            }
         }
         set
         {
            if ( 0 <= i && i < limit )
            {
               subtrie[ i ] = value;
            }
            else
            {
               InvalidIndex( i );
            }
         }
      }// this

      /// <summary>Signal an invalid index of a trie node. 
      /// </summary>
      /// <param name="i">Invalid index.
      /// </param>
      private void InvalidIndex( int i )
      {
         //MessageBox.Show( String.Format( "Index {0} is out of [0 .. {1}] range", i, limit - 1 ) );
      }// InvalidIndex
   }// TrieNode (class
}// TriesLib (namespace)
