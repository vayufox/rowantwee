/* MacKeyboardHook for SI3 
 * Rev 2 2015-07-28
 * Copyright ©  == do as you will, I care not! -- Thank me or mention me! :)
 * 
 * To contact me, see here:  http://forum.unity3d.com/members/inventor2010.27746/
 *
 */

using UnityEngine;
using UnityEditor;

using System;
using System.Runtime.InteropServices;

namespace ScriptInspectorMods
{
	
	[InitializeOnLoad]
	internal static class MacKeyboardHook
	{
#if UNITY_EDITOR_OSX
		
		//Keycodes excerpt from Events.h:
		//ALL Apple Codes here (on a mac): /System/Library/Frameworks/Carbon.framework/Versions/A/Frameworks/HIToolbox.framework/Versions/A/Headers/Events.h
		/*
		 *  Summary:
		 *    Virtual keycodes
		 *
		 *  Discussion:
		 *    These constants are the virtual keycodes defined originally in
		 *    Inside Mac Volume V, pg. V-191. They identify physical keys on a
		 *    keyboard. Those constants with "ANSI" in the name are labeled
		 *    according to the key position on an ANSI-standard US keyboard.
		 *    For example, kVK_ANSI_A indicates the virtual keycode for the key
		 *    with the letter 'A' in the US keyboard layout. Other keyboard
		 *    layouts may have the 'A' key label on a different physical key;
		 *    in this case, pressing 'A' will generate a different virtual
		 *    keycode.
		 */
		enum macVirtualKeycodes
		{
			kVK_ANSI_A                    = 0x00,
			kVK_ANSI_S                    = 0x01,
			kVK_ANSI_D                    = 0x02,
			kVK_ANSI_F                    = 0x03,
			kVK_ANSI_H                    = 0x04,
			kVK_ANSI_G                    = 0x05,
			kVK_ANSI_Z                    = 0x06,
			kVK_ANSI_X                    = 0x07,
			kVK_ANSI_C                    = 0x08,
			kVK_ANSI_V                    = 0x09,
			kVK_ANSI_B                    = 0x0B,
			kVK_ANSI_Q                    = 0x0C,
			kVK_ANSI_W                    = 0x0D,
			kVK_ANSI_E                    = 0x0E,
			kVK_ANSI_R                    = 0x0F,
			kVK_ANSI_Y                    = 0x10,
			kVK_ANSI_T                    = 0x11,
			kVK_ANSI_1                    = 0x12,
			kVK_ANSI_2                    = 0x13,
			kVK_ANSI_3                    = 0x14,
			kVK_ANSI_4                    = 0x15,
			kVK_ANSI_6                    = 0x16,
			kVK_ANSI_5                    = 0x17,
			kVK_ANSI_Equal                = 0x18,
			kVK_ANSI_9                    = 0x19,
			kVK_ANSI_7                    = 0x1A,
			kVK_ANSI_Minus                = 0x1B,
			kVK_ANSI_8                    = 0x1C,
			kVK_ANSI_0                    = 0x1D,
			kVK_ANSI_RightBracket         = 0x1E,
			kVK_ANSI_O                    = 0x1F,
			kVK_ANSI_U                    = 0x20,
			kVK_ANSI_LeftBracket          = 0x21,
			kVK_ANSI_I                    = 0x22,
			kVK_ANSI_P                    = 0x23,
			kVK_ANSI_L                    = 0x25,
			kVK_ANSI_J                    = 0x26,
			kVK_ANSI_Quote                = 0x27,
			kVK_ANSI_K                    = 0x28,
			kVK_ANSI_Semicolon            = 0x29,
			kVK_ANSI_Backslash            = 0x2A,
			kVK_ANSI_Comma                = 0x2B,
			kVK_ANSI_Slash                = 0x2C,
			kVK_ANSI_N                    = 0x2D,
			kVK_ANSI_M                    = 0x2E,
			kVK_ANSI_Period               = 0x2F,
			kVK_ANSI_Grave                = 0x32,
			kVK_ANSI_KeypadDecimal        = 0x41,
			kVK_ANSI_KeypadMultiply       = 0x43,
			kVK_ANSI_KeypadPlus           = 0x45,
			kVK_ANSI_KeypadClear          = 0x47,
			kVK_ANSI_KeypadDivide         = 0x4B,
			kVK_ANSI_KeypadEnter          = 0x4C,
			kVK_ANSI_KeypadMinus          = 0x4E,
			kVK_ANSI_KeypadEquals         = 0x51,
			kVK_ANSI_Keypad0              = 0x52,
			kVK_ANSI_Keypad1              = 0x53,
			kVK_ANSI_Keypad2              = 0x54,
			kVK_ANSI_Keypad3              = 0x55,
			kVK_ANSI_Keypad4              = 0x56,
			kVK_ANSI_Keypad5              = 0x57,
			kVK_ANSI_Keypad6              = 0x58,
			kVK_ANSI_Keypad7              = 0x59,
			kVK_ANSI_Keypad8              = 0x5B,
			kVK_ANSI_Keypad9              = 0x5C
		};
		
	/* keycodes for keys that are independent of keyboard layout*/
		private	enum macIndependentKeycodes
		{
			kVK_Return                    = 0x24,
			kVK_Tab                       = 0x30,
			kVK_Space                     = 0x31,
			kVK_Delete                    = 0x33,
			kVK_Escape                    = 0x35,
			kVK_Command                   = 0x37,
			kVK_Shift                     = 0x38,
			kVK_CapsLock                  = 0x39,
			kVK_Option                    = 0x3A,
			kVK_Control                   = 0x3B,
			kVK_RightShift                = 0x3C,
			kVK_RightOption               = 0x3D,
			kVK_RightControl              = 0x3E,
			kVK_Function                  = 0x3F,
			kVK_F17                       = 0x40,
			kVK_VolumeUp                  = 0x48,
			kVK_VolumeDown                = 0x49,
			kVK_Mute                      = 0x4A,
			kVK_F18                       = 0x4F,
			kVK_F19                       = 0x50,
			kVK_F20                       = 0x5A,
			kVK_F5                        = 0x60,
			kVK_F6                        = 0x61,
			kVK_F7                        = 0x62,
			kVK_F3                        = 0x63,
			kVK_F8                        = 0x64,
			kVK_F9                        = 0x65,
			kVK_F11                       = 0x67,
			kVK_F13                       = 0x69,
			kVK_F16                       = 0x6A,
			kVK_F14                       = 0x6B,
			kVK_F10                       = 0x6D,
			kVK_F12                       = 0x6F,
			kVK_F15                       = 0x71,
			kVK_Help                      = 0x72,
			kVK_Home                      = 0x73,
			kVK_PageUp                    = 0x74,
			kVK_ForwardDelete             = 0x75,
			kVK_F4                        = 0x76,
			kVK_End                       = 0x77,
			kVK_F2                        = 0x78,
			kVK_PageDown                  = 0x79,
			kVK_F1                        = 0x7A,
			kVK_LeftArrow                 = 0x7B,
			kVK_RightArrow                = 0x7C,
			kVK_DownArrow                 = 0x7D,
			kVK_UpArrow                   = 0x7E
		};
		
		
		
		private enum macModifiersOnBit
		{
		/* modifiers */
			activeFlagBit                 = 0,    /* activate? (activateEvt and mouseDown)*/
			btnStateBit                   = 7,    /* state of button?*/
			cmdKeyBit                     = 8,    /* command key down?*/
			shiftKeyBit                   = 9,    /* shift key down?*/
			alphaLockBit                  = 10,   /* alpha lock down?*/
			optionKeyBit                  = 11,   /* option key down?*/
			controlKeyBit                 = 12,   /* control key down?*/
			rightShiftKeyBit              = 13,   /* right shift key down? Not supported on Mac OS X.*/
			rightOptionKeyBit             = 14,   /* right Option key down? Not supported on Mac OS X.*/
			rightControlKeyBit            = 15    /* right Control key down? Not supported on Mac OS X.*/
		};
		
		private enum macModifiers
		{
			activeFlag                    = 1 << macModifiersOnBit.activeFlagBit,
			btnState                      = 1 << macModifiersOnBit.btnStateBit,
			cmdKey                        = 1 << macModifiersOnBit.cmdKeyBit,
			shiftKey                      = 1 << macModifiersOnBit.shiftKeyBit,
			alphaLock                     = 1 << macModifiersOnBit.alphaLockBit,
			optionKey                     = 1 << macModifiersOnBit.optionKeyBit,
			controlKey                    = 1 << macModifiersOnBit.controlKeyBit,
			rightShiftKey                 = 1 << macModifiersOnBit.rightShiftKeyBit, /* Not supported on Mac OS X.*/
			rightOptionKey                = 1 << macModifiersOnBit.rightOptionKeyBit, /* Not supported on Mac OS X.*/
			rightControlKey               = 1 << macModifiersOnBit.rightControlKeyBit /* Not supported on Mac OS X.*/
		};
		
		
		public delegate void KeyboardHook_KeyPressedDelegate( uint hotKey, uint modifiers );
		
		[DllImport ("MacKeyboardHook")]
		private static extern void StartHook();
		[DllImport ("MacKeyboardHook")]
		private static extern void StopHook();
		[DllImport ("MacKeyboardHook")]
		private static extern void SetWatchesApplicationActivity(bool watchAppActivity);
		[DllImport ("MacKeyboardHook")]
		private static extern void SetKeyPressDelegate( KeyboardHook_KeyPressedDelegate del);
		[DllImport ("MacKeyboardHook")]
		private static extern void UnSetKeyPressDelegate();
		[DllImport ("MacKeyboardHook")]
		private static extern void AddKeyCombo(uint hotKey, uint modifiers);
		[DllImport ("MacKeyboardHook")]
		private static extern void RemoveKeyCombo(uint hotKey, uint modifiers);
		
		static MacKeyboardHook()
		{
			if (Application.platform == RuntimePlatform.OSXEditor)
			{
				EditorApplication.update += SetHookOnFirstUpdate;
				EditorApplication.update += OnUpdate;
				AppDomain.CurrentDomain.DomainUnload += OnDomainUnload;
				
				StopHook(); //Ensure the hook isn't somehow lingering from any previous activity
				HookRunning = false;
			}
		}
		
		static void SetHookOnFirstUpdate()
		{
			EditorApplication.update -= SetHookOnFirstUpdate;
			
			SetKeyPressDelegate( new KeyboardHook_KeyPressedDelegate(KeyPressed) );
			
			// Add our keycombos to listen for:
			AddKeyCombo((uint)macVirtualKeycodes.kVK_ANSI_S, (uint)macModifiers.cmdKey);
			AddKeyCombo((uint)macVirtualKeycodes.kVK_ANSI_Z, (uint)macModifiers.cmdKey);
			AddKeyCombo((uint)macVirtualKeycodes.kVK_ANSI_Z, (uint)macModifiers.cmdKey+(uint)macModifiers.shiftKey);
		}
		
		static bool HookRunning = false;
		
		static bool ShouldRun()
		{
			var wnd = EditorWindow.focusedWindow;
			if (wnd != null &&
			(FGTextBuffer.activeEditor != null &&
				FGTextBuffer.activeEditor.hasCodeViewFocus &&
				wnd == FGTextBuffer.activeEditor.OwnerWindow
				//|| vkCode == '\t' && (wnd is FGConsole || wnd is TabSwitcher)))
			))
			{
				return true;
			}
			return false;
		}
		
		//Our hotkey delegate method (all global hot keys that we register will block any defult usage by defult (they will be "used") while the "hook" is running. Pass them on to the current window here):
		static void KeyPressed( uint hotKey, uint modifiers )
		{
			//Debug.Log("Yay hotKey: " + hotKey + " With Modifiers:" + modifiers + " was pressed!");
			
			//Convert our native Key Combo's to their standard SI equivalent, and pass them to the top most SI window:
			if(ShouldRun()) //Hook Must be running or we wouldn't be here
			{
				sendToWindow = EditorWindow.focusedWindow;
				if((modifiers & (uint)macModifiers.cmdKey) > 0)
				{
					if(hotKey == (uint)macVirtualKeycodes.kVK_ANSI_Z)
					{
						if((modifiers & (uint)macModifiers.shiftKey) > 0)
						{
							delayedKeyEvent = new Event();
							delayedKeyEvent.type = EventType.KeyDown;
							delayedKeyEvent.keyCode = KeyCode.Z;
							delayedKeyEvent.modifiers = EventModifiers.Command | EventModifiers.Alt | EventModifiers.Shift;
						}
						else
						{
							delayedKeyEvent = new Event();
							delayedKeyEvent.type = EventType.KeyDown;
							delayedKeyEvent.keyCode = KeyCode.Z;
							delayedKeyEvent.modifiers = EventModifiers.Command | EventModifiers.Alt;
						}
					}
					
					if(hotKey == (uint)macVirtualKeycodes.kVK_ANSI_S)
					{
						delayedKeyEvent = new Event();
						delayedKeyEvent.type = EventType.KeyDown;
						delayedKeyEvent.keyCode = KeyCode.S;
						delayedKeyEvent.modifiers = EventModifiers.Control;
					}
				}
				
			}
		}
		
		static EditorWindow sendToWindow;
		static Event delayedKeyEvent;
		static void OnUpdate()
		{
			if (delayedKeyEvent != null) //Should run already checked at this point or we wouldn't have a delayedKeyEvent
			{
				var temp = delayedKeyEvent;
				delayedKeyEvent = null;
				if (sendToWindow && sendToWindow == EditorWindow.focusedWindow)
				{
					//Debug.Log("Forwarding " + temp);
					sendToWindow.SendEvent(temp);
				}
			}
			
			if(ShouldRun() && !HookRunning)
			{
				StartHook();
				HookRunning = true;
			}
			else if(!ShouldRun() && HookRunning)
			{
				StopHook();
				HookRunning = false;
			}
		}
		
		static void OnDomainUnload(object sender, EventArgs e)
		{
			StopHook(); //Ensure the hook is off since we're done... No reason to use or trust our state bool, The bundle has it's own.
			HookRunning = false;
		}
#endif
	}
	
}
