﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.ILSpy.Debugger.Models.TreeModel;

namespace ICSharpCode.ILSpy.Debugger.Tooltips
{
	/// <summary>
	/// Popup containing <see cref="DebuggerTooltipControl"></see>.
	/// </summary>
	internal class DebuggerPopup : Popup
	{
		internal DebuggerTooltipControl contentControl;

		public DebuggerPopup(DebuggerTooltipControl parentControl, TextLocation logicalPosition, bool showPins = true)
		{
			this.contentControl = new DebuggerTooltipControl(parentControl, logicalPosition, showPins);
			this.contentControl.containingPopup = this;
			this.Child = this.contentControl;
			this.IsLeaf = false;
		}

		public IEnumerable<ITreeNode> ItemsSource
		{
			get { return this.contentControl.ItemsSource; }
			set { this.contentControl.SetItemsSource(value); }
		}

		private bool isLeaf;
		public bool IsLeaf
		{
			get { return isLeaf; }
			set
			{
				isLeaf = value;
				// leaf popup closes on lost focus
				this.StaysOpen = !isLeaf;
			}
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			if (isLeaf) {
				this.contentControl.CloseOnLostFocus();
			}
		}

		public void Open()
		{
			this.IsOpen = true;
		}

		public void CloseSelfAndChildren()
		{
			this.contentControl.CloseChildPopups();
			this.IsOpen = false;
		}
	}
}