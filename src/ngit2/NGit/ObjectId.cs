/*
This code is derived from jgit (http://eclipse.org/jgit).
Copyright owners are documented in jgit's IP log.

This program and the accompanying materials are made available
under the terms of the Eclipse Distribution License v1.0 which
accompanies this distribution, is reproduced below, and is
available at http://www.eclipse.org/org/documents/edl-v10.php

All rights reserved.

Redistribution and use in source and binary forms, with or
without modification, are permitted provided that the following
conditions are met:

- Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer.

- Redistributions in binary form must reproduce the above
  copyright notice, this list of conditions and the following
  disclaimer in the documentation and/or other materials provided
  with the distribution.

- Neither the name of the Eclipse Foundation, Inc. nor the
  names of its contributors may be used to endorse or promote
  products derived from this software without specific prior
  written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System;
using System.Linq;
using System.Text;
using NGit;
// using NGit.Errors;
using NGit.Util;

namespace NGit
{
	/// <summary>A SHA-1 abstraction.</summary>
	/// <remarks>A SHA-1 abstraction.</remarks>
	[System.Runtime.Serialization.DataContract]
	public class ObjectId : AnyObjectId
	{
		private const long serialVersionUID = 1L;

		private static readonly NGit.ObjectId ZEROID;

		private static readonly string ZEROID_STR;

		static ObjectId()
		{
            byte[] zeros = Enumerable.Repeat<byte>(0x00, Constants.OBJECT_ID_LENGTH).ToArray();
            
			ZEROID = new NGit.ObjectId(zeros);
			ZEROID_STR = ZEROID.Name;
		}

		/// <summary>Get the special all-null ObjectId.</summary>
		/// <remarks>Get the special all-null ObjectId.</remarks>
		/// <returns>the all-null ObjectId, often used to stand-in for no object.</returns>
		public static NGit.ObjectId ZeroId
		{
			get
			{
				return ZEROID;
			}
		}

		/// <summary>Test a string of characters to verify it is a hex format.</summary>
		/// <remarks>
		/// Test a string of characters to verify it is a hex format.
		/// <p>
		/// If true the string can be parsed with
		/// <see cref="FromString(string)">FromString(string)</see>
		/// .
		/// </remarks>
		/// <param name="id">the string to test.</param>
		/// <returns>true if the string can converted into an ObjectId.</returns>
		public static bool IsId(string id)
		{
			if (id.Length != Constants.OBJECT_ID_STRING_LENGTH)
			{
				return false;
			}
			try
			{
				for (int i = 0; i < Constants.OBJECT_ID_STRING_LENGTH; i++)
				{
                    Convert.ToInt32(id.Substring(i, 1), 16);
                }
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>Convert an ObjectId into a hex string representation.</summary>
		/// <remarks>Convert an ObjectId into a hex string representation.</remarks>
		/// <param name="i">the id to convert. May be null.</param>
		/// <returns>the hex string conversion of this id's content.</returns>
		public static string ToString(NGit.ObjectId i)
		{
			return i != null ? i.Name : ZEROID_STR;
		}

		/// <summary>Compare to object identifier byte sequences for equality.</summary>
		/// <remarks>Compare to object identifier byte sequences for equality.</remarks>
		/// <param name="firstBuffer">
		/// the first buffer to compare against. Must have at least 20
		/// bytes from position ai through the end of the buffer.
		/// </param>
		/// <param name="fi">first offset within firstBuffer to begin testing.</param>
		/// <param name="secondBuffer">
		/// the second buffer to compare against. Must have at least 2
		/// bytes from position bi through the end of the buffer.
		/// </param>
		/// <param name="si">first offset within secondBuffer to begin testing.</param>
		/// <returns>true if the two identifiers are the same.</returns>
		public static bool Equals(byte[] firstBuffer, int fi, byte[] secondBuffer, int si
			)
		{
			return firstBuffer[fi] == secondBuffer[si] && firstBuffer[fi + 1] == secondBuffer
				[si + 1] && firstBuffer[fi + 2] == secondBuffer[si + 2] && firstBuffer[fi + 3] ==
				 secondBuffer[si + 3] && firstBuffer[fi + 4] == secondBuffer[si + 4] && firstBuffer
				[fi + 5] == secondBuffer[si + 5] && firstBuffer[fi + 6] == secondBuffer[si + 6] 
				&& firstBuffer[fi + 7] == secondBuffer[si + 7] && firstBuffer[fi + 8] == secondBuffer
				[si + 8] && firstBuffer[fi + 9] == secondBuffer[si + 9] && firstBuffer[fi + 10] 
				== secondBuffer[si + 10] && firstBuffer[fi + 11] == secondBuffer[si + 11] && firstBuffer
				[fi + 12] == secondBuffer[si + 12] && firstBuffer[fi + 13] == secondBuffer[si + 
				13] && firstBuffer[fi + 14] == secondBuffer[si + 14] && firstBuffer[fi + 15] == 
				secondBuffer[si + 15] && firstBuffer[fi + 16] == secondBuffer[si + 16] && firstBuffer
				[fi + 17] == secondBuffer[si + 17] && firstBuffer[fi + 18] == secondBuffer[si + 
				18] && firstBuffer[fi + 19] == secondBuffer[si + 19];
		}

        /// <summary>Convert an ObjectId from raw binary representation.</summary>
        /// <remarks>Convert an ObjectId from raw binary representation.</remarks>
        /// <param name="bs">
        /// the raw byte buffer to read from. At least 20 bytes must be
        /// available within this byte array.
        /// </param>
        /// <returns>the converted object id.</returns>
        public static NGit.ObjectId FromRaw(byte[] bs)
        {
            return FromRaw(bs, 0);
        }

        /// <summary>Convert an ObjectId from raw binary representation.</summary>
        /// <remarks>Convert an ObjectId from raw binary representation.</remarks>
        /// <param name="bs">
        /// the raw byte buffer to read from. At least 20 bytes after p
        /// must be available within this byte array.
        /// </param>
        /// <param name="p">position to read the first byte of data from.</param>
        /// <returns>the converted object id.</returns>
        public static NGit.ObjectId FromRaw(byte[] bs, int p)
        {
            if (bs.Length < p + Constants.OBJECT_ID_LENGTH)
            {
                throw new ArgumentException("byte buf not long enough");
            }

            return new ObjectId(bs.Skip(p).Take(Constants.OBJECT_ID_LENGTH).ToArray());
        }

        /// <summary>Convert an ObjectId from hex characters (US-ASCII).</summary>
        /// <remarks>Convert an ObjectId from hex characters (US-ASCII).</remarks>
        /// <param name="buf">
        /// the US-ASCII buffer to read from. At least 40 bytes after
        /// offset must be available within this byte array.
        /// </param>
        /// <param name="offset">position to read the first character from.</param>
        /// <returns>the converted object id.</returns>
        public static NGit.ObjectId FromString(byte[] buf, int offset)
        {
            if (buf.Length - offset < Constants.OBJECT_ID_STRING_LENGTH)
            {
                throw new ArgumentException("Byte buf not long enough");
            }
            byte[] b = Enumerable.Range(offset, offset + Constants.OBJECT_ID_STRING_LENGTH)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(Encoding.ASCII.GetString(buf.Skip(x).Take(2).ToArray()), 16))
                .ToArray();
            return new ObjectId(b);
        }

        /// <summary>Convert an ObjectId from hex characters.</summary>
        /// <remarks>Convert an ObjectId from hex characters.</remarks>
        /// <param name="str">the string to read from. Must be 40 characters long.</param>
        /// <returns>the converted object id.</returns>
        public static NGit.ObjectId FromString(string str)
        {
            if (str.Length != Constants.OBJECT_ID_STRING_LENGTH)
            {
                throw new ArgumentException("Invalid id: " + str);
            }
            byte[] b = Enumerable.Range(0, str.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                .ToArray();
            return new ObjectId(b);
        }

        internal ObjectId(byte[] bytes)
        {
            Buffer.BlockCopy(bytes, 0, this.sha1, 0, Constants.OBJECT_ID_LENGTH);
        }

        /// <summary>Initialize this instance by copying another existing ObjectId.</summary>
        /// <remarks>
        /// Initialize this instance by copying another existing ObjectId.
        /// <p>
        /// This constructor is mostly useful for subclasses who want to extend an
        /// ObjectId with more properties, but initialize from an existing ObjectId
        /// instance acquired by other means.
        /// </remarks>
        /// <param name="src">another already parsed ObjectId to copy the value out of.</param>
        protected internal ObjectId(AnyObjectId src)
		{
            Buffer.BlockCopy(src.sha1, 0, this.sha1, 0, Constants.OBJECT_ID_LENGTH);
		}

		public override NGit.ObjectId ToObjectId()
		{
			return this;
		}

		/// <exception cref="System.IO.IOException"></exception>
		//private void WriteObject(ObjectOutputStream os)
		//{
		//	os.WriteInt(w1);
		//	os.WriteInt(w2);
		//	os.WriteInt(w3);
		//	os.WriteInt(w4);
		//	os.WriteInt(w5);
		//}

		/// <exception cref="System.IO.IOException"></exception>
		//private void ReadObject(ObjectInputStream ois)
		//{
		//	w1 = ois.ReadInt();
		//	w2 = ois.ReadInt();
		//	w3 = ois.ReadInt();
		//	w4 = ois.ReadInt();
		//	w5 = ois.ReadInt();
		//}
	}
}
