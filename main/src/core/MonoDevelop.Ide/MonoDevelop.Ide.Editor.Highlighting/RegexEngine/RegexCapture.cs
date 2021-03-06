//------------------------------------------------------------------------------
// <copyright file="RegexCapture.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

// Capture is just a location/length pair that indicates the
// location of a regular expression match. A single regexp
// search may return multiple Capture within each capturing
// RegexGroup.

namespace MonoDevelop.Ide.Editor.Highlighting.RegexEngine {
	using System;
	using System.Text;
	using MonoDevelop.Core.Text;

	/// <devdoc>
	///    <para> 
	///       Represents the results from a single subexpression capture. The object represents
	///       one substring for a single successful capture.</para>
	/// </devdoc>
#if !SILVERLIGHT
	[ Serializable() ] 
#endif
    class Capture {
        internal ITextSource _text;
        internal int _index;
        internal int _length;

        internal Capture(ITextSource text, int i, int l) {
            _text = text;
            _index = i;
            _length = l;
        }

        /*
         * The index of the beginning of the matched capture
         */
        /// <devdoc>
        ///    <para>Returns the position in the original string where the first character of
        ///       captured substring was found.</para>
        /// </devdoc>
        public int Index {
            get {
                return _index;
            }
        }

        /*
         * The length of the matched capture
         */
        /// <devdoc>
        ///    <para>
        ///       Returns the length of the captured substring.
        ///    </para>
        /// </devdoc>
        public int Length {
            get {
                return _length;
            }
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public string Value {
            get {
				return _text.GetTextAt (_index, _length);
            }
        }

        /*
         * The capture as a string
         */
        /// <devdoc>
        ///    <para>
        ///       Returns 
        ///          the substring that was matched.
        ///       </para>
        ///    </devdoc>
        override public String ToString() {
            return Value;
        }

		public ITextSource ToTextSource() {
			return _text.CreateSnapshot (_index, _length);
		}

        /*
         * The original string
         */
        internal ITextSource GetOriginalString() {
            return _text;
        }

        /*
         * The substring to the left of the capture
         */
        internal ITextSource GetLeftSubstring() {
			return _text.CreateSnapshot(0, _index);
        }

        /*
         * The substring to the right of the capture
         */
        internal ITextSource GetRightSubstring() {
			return _text.CreateSnapshot(_index + _length, _text.Length - _index - _length);
        }

#if DBG
        internal virtual String Description() {
            StringBuilder Sb = new StringBuilder();

            Sb.Append("(I = ");
            Sb.Append(_index);
            Sb.Append(", L = ");
            Sb.Append(_length);
            Sb.Append("): ");
            Sb.Append(_text, _index, _length);

            return Sb.ToString();
        }
#endif
    }



}
