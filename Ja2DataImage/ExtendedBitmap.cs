﻿using Ja2Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ja2DataImage
{
	public class ExtendedBitmap
	{
		public ExtendedBitmap()
		{
		}
		public ExtendedBitmap(BitmapFrame bm, Int16 offsetX, Int16 offsetY)
		{
			this.Frame = bm;
			this.OffsetX = offsetX;
			this.OffsetY = offsetY;
		}

		public ExtendedBitmap(BitmapFrame bm, StciRgb rgb)
		{
			this.Frame = bm;
			this.RgbData = rgb;
		}

		private ExtendedBitmap(ExtendedBitmap old)
		{
			this.Frame = (BitmapFrame)old.Frame.Clone();
			this.OffsetX = old.OffsetX;
			this.OffsetY = old.OffsetY;
			this.id = old.Id;
			this.ApplicationData = old.ApplicationData;

			this.RgbData = old.RgbData;
		}

		string id;
		public string Id
		{
			get
			{
				if (id == null)
					id = Guid.NewGuid().ToString();
				return id;
			}
		}

		private UInt32 FTransparentColorIndex = 0;
		public UInt32 TransparentColorIndex
		{
			get { return this.FTransparentColorIndex; }
			set { this.FTransparentColorIndex = value; }
		}

		private BitmapFrame _bm;
		public BitmapFrame Frame
		{
			get
			{
				return _bm;
			}
			set
			{
				_bm = value;
			}
		}

		public Int16 OffsetX;
		public Int16 OffsetY;
		public AuxObjectData ApplicationData;
		// RgbData
		public StciRgb RgbData;

		public bool IsForeshorteningStarter
		{
			get { return (ApplicationData.Flags |= AuxObjectFlags.AUX_ANIMATED_TILE) != 0; }
		}

		public int ForeshorteningLength
		{
			get { return ApplicationData.NumberOfFrames; }
		}

		public virtual ExtendedBitmap Clone()
		{
			ExtendedBitmap newExBm = new ExtendedBitmap(this);
			newExBm.TransparentColorIndex = this.TransparentColorIndex;
			return newExBm;
		}

		// Trim background pixels
		public void Trim()
		{
			Color bc = this.Frame.Palette.Colors[0];
			int _top = -1;
			int _left = this.Frame.PixelWidth;
			byte[] imageData = new byte[this.Frame.PixelWidth * this.Frame.PixelHeight];
			this.Frame.CopyPixels(imageData, this.Frame.PixelWidth, 0);

			for (int i = 0; i < this.Frame.PixelHeight; i++)
			{
				for (int j = 0; j < this.Frame.PixelWidth; j++)
				{
					byte colorIndex = imageData[i * this.Frame.PixelWidth + j];
					Color c = this.Frame.Palette.Colors[colorIndex];
					if (c != bc)
					{
						if (_top < 0)
							_top = i;

						if (_left > j)
							_left = j;

						continue;
					}
				}
			}

			int _bottom = this.Frame.PixelHeight;
			int _right = 0;

			for (int i = this.Frame.PixelHeight - 1; i > 0; i--)
			{
				for (int j = this.Frame.PixelWidth - 1; j > 0; j--)
				{
					byte colorIndex = imageData[i * this.Frame.PixelWidth + j];
					Color c = this.Frame.Palette.Colors[colorIndex];
					if (c != bc)
					{
						if (_bottom == this.Frame.PixelHeight)
							_bottom = i;

						if (_right < j)
							_right = j;
					}
				}
			}

			int _width = _right - _left;
			int _height = _bottom - _top;

			this.OffsetX += (short)(_left - this.Frame.Width / 2);
			this.OffsetY += (short)(_top - this.Frame.Height / 2);

			byte[] trimedData = new byte[_width * _height];
			this.Frame.CopyPixels(new Int32Rect(_left, _top, _width, _height), trimedData, _width, 0);
			var source = BitmapFrame.Create(
				_width, _height, 96, 96, PixelFormats.Indexed8, this.Frame.Palette, trimedData, _width);
			this.Frame = BitmapFrame.Create(source);
		}

		public static List<ExtendedBitmap> ConvertStciIndexedToBitmaps(StciIndexed aStci)
		{
			var _result = new List<ExtendedBitmap>(aStci.Images.Length);

			var _stciPalette = aStci.Palette;
			var _colors = new List<Color>(StciIndexed.NUMBER_OF_COLORS);
			for (int i = 0; i < StciIndexed.NUMBER_OF_COLORS; i++)
				_colors.Add(Color.FromRgb(_stciPalette[i * 3], _stciPalette[i * 3 + 1], _stciPalette[i * 3 + 2]));
			var _palette = new BitmapPalette(_colors);

			foreach(var _subImage in aStci.Images)
			{
				var _header = _subImage.Header;

				var _imageSource = BitmapSource.Create(
					_header.Width, 
					_header.Height, 
					96, 96, 
					PixelFormats.Indexed8, 
					_palette, 
					_subImage.ImageData, 
					_header.Width);

				var _frame = BitmapFrame.Create(_imageSource);
				var _bm = new ExtendedBitmap(_frame, _header.OffsetX, _header.OffsetY);
				_bm.TransparentColorIndex = (uint)aStci.Header.TransparentColorIndex;
				_bm.ApplicationData = _subImage.AuxData;
				_result.Add(_bm);
			}

			return _result;
		}

		public static StciIndexed ConvertBitmapsToStciIndexed(List<ExtendedBitmap> aBitmaps, bool aIsTransparent, bool aIsTrim)
		{
			if (aBitmaps.Count == 0)
				return null;

			var _subHeader = new StciIndexedHeader((ushort)aBitmaps.Count);
			var _palette = new byte[StciIndexed.NUMBER_OF_COLORS * 3];
			for(int i = 0; i < StciIndexed.NUMBER_OF_COLORS; i++)
			{
				var _color = aBitmaps[0].Frame.Palette.Colors[i];
				_palette[i * 3] = _color.R;
				_palette[i * 3 + 1] = _color.G;
				_palette[i * 3 + 2] = _color.B;
			}
			var _appDataSize = 0;
			if (aBitmaps[0].ApplicationData != null)
				_appDataSize = aBitmaps.Count * 16;

			var _header = new StciHeader(0, aBitmaps[0].TransparentColorIndex, (uint)_appDataSize, _subHeader);

			if (aIsTransparent)
				_header.Flags |= StciFlags.STCI_TRANSPARENT;

			var _subImages = new StciSubImage[aBitmaps.Count];
			for(int i = 0; i < aBitmaps.Count; i++)
			{
				if (aIsTrim)
					aBitmaps[i].Trim();

				var _subImageHeader = new StciSubImageHeader();
				_subImageHeader.OffsetX = aBitmaps[i].OffsetX;
				_subImageHeader.OffsetY = aBitmaps[i].OffsetY;
				_subImageHeader.Width = (ushort)aBitmaps[i].Frame.PixelWidth;
				_subImageHeader.Height = (ushort)aBitmaps[i].Frame.PixelHeight;

				var _subImage = new StciSubImage(_subImageHeader);
				_subImage.ImageData = new byte[_subImage.Header.Width * _subImage.Header.Height];
				aBitmaps[i].Frame.CopyPixels(_subImage.ImageData, _subImage.Header.Width, 0);
				_subImage.AuxData = aBitmaps[i].ApplicationData;
				_subImages[i] = _subImage;
			}

			var _stci = new StciIndexed(_header, _palette, _subImages);
			return _stci;
		}

		public static GifBitmapCoder ConvertStciIndexedToGifCoder(StciIndexed aStci, UInt16 aDelay, bool aUseTransparent)
		{
			var _stciPalette = aStci.Palette;
			var _colors = new List<Color>(StciIndexed.NUMBER_OF_COLORS);
			for (int i = 0; i < StciIndexed.NUMBER_OF_COLORS; i++)
				_colors.Add(Color.FromRgb(_stciPalette[i * 3], _stciPalette[i * 3 + 1], _stciPalette[i * 3 + 2]));
			var _palette = new BitmapPalette(_colors);

			var _gifCoder = new GifBitmapCoder(_palette);
			_gifCoder.Extentions.Add(new GifCommentExtension("Egorov A. V. for ja2.su"));
			int j = 0;
			foreach (var _subImage in aStci.Images)
			{
				var _header = _subImage.Header;

				var _imageSource = BitmapSource.Create(
					_header.Width,
					_header.Height,
					96, 96,
					PixelFormats.Indexed8,
					_palette,
					_subImage.ImageData,
					_header.Width);

				var _frame = BitmapFrame.Create(_imageSource);
				var _bf = new GifBitmapFrame(_frame, _header.OffsetX, _header.OffsetY);
				_bf.TransparentColorIndex = (byte)aStci.Header.TransparentColorIndex;

				var _auxData = new byte[AuxObjectData.SIZE];
				_subImage.AuxData.Save(new MemoryStream(_auxData));
				_bf.Extensions.Add(new GifApplicationExtension("STI_EDIT1.0", _auxData));
				_bf.DisposalMethod = GifFrameDisposalMethod.RestoreToBackgroundColor;
				_bf.UseGlobalPalette = true;
				_bf.UseTransparency = aUseTransparent;
				_bf.Delay = aDelay;
				if (aUseTransparent)
					_bf.TransparentColorIndex = (byte)aStci.Header.TransparentColorIndex;
				_gifCoder.AddFrame(_bf);
			}

			return _gifCoder;
		}

		public static StciIndexed ConvertGifFramesToStciIndexed(List<GifBitmapFrame> aBitmaps, bool aIsTransparent, bool aIsTrim)
		{
			if (aBitmaps.Count == 0)
				return null;

			var _subHeader = new StciIndexedHeader((ushort)aBitmaps.Count);
			var _palette = new byte[StciIndexed.NUMBER_OF_COLORS * 3];
			for (int i = 0; i < StciIndexed.NUMBER_OF_COLORS; i++)
			{
				var _color = aBitmaps[0].Frame.Palette.Colors[i];
				_palette[i * 3] = _color.R;
				_palette[i * 3 + 1] = _color.G;
				_palette[i * 3 + 2] = _color.B;
			}
			var _appDataSize = 0;
			if (aBitmaps[0].Extensions.FirstOrDefault(
					x => x.ExtensionType == ExtensionType.ApplicationExtension && 
						((GifApplicationExtension)x).ApplicationId == "STI_EDIT1.0") != null)
				_appDataSize = aBitmaps.Count * 16;

			var _header = new StciHeader(0, aBitmaps[0].TransparentColorIndex, (uint)_appDataSize, _subHeader);

			if (aIsTransparent)
				_header.Flags |= StciFlags.STCI_TRANSPARENT;

			var _subImages = new StciSubImage[aBitmaps.Count];
			for (int i = 0; i < aBitmaps.Count; i++)
			{
				if (aIsTrim)
					aBitmaps[i].Trim();

				var _subImageHeader = new StciSubImageHeader();
				_subImageHeader.OffsetX = aBitmaps[i].OffsetX;
				_subImageHeader.OffsetY = aBitmaps[i].OffsetY;
				_subImageHeader.Width = (ushort)aBitmaps[i].Frame.PixelWidth;
				_subImageHeader.Height = (ushort)aBitmaps[i].Frame.PixelHeight;

				var _subImage = new StciSubImage(_subImageHeader);
				_subImage.ImageData = new byte[_subImage.Header.Width * _subImage.Header.Height];
				aBitmaps[i].Frame.CopyPixels(_subImage.ImageData, _subImage.Header.Width, 0);

				var _appDataExt = aBitmaps[i].Extensions.FirstOrDefault(x => 
					x.ExtensionType == ExtensionType.ApplicationExtension &&
						((GifApplicationExtension)x).ApplicationId == "STI_EDIT1.0");
				if (_appDataExt != null)
				{
					var _auxData = new AuxObjectData();
					_auxData.Load(new MemoryStream(_appDataExt.Data));
					_subImage.AuxData = _auxData;
				}
				else
					_subImage.AuxData = null;

				_subImages[i] = _subImage;
			}

			var _stci = new StciIndexed(_header, _palette, _subImages);
			return _stci;
		}
		
	}
}
