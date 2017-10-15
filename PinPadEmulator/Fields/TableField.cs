using PinPadEmulator.Commands;
using PinPadEmulator.Extensions;
using PinPadEmulator.Utils;
using System;
using System.Text;

namespace PinPadEmulator.Fields
{
	public class TableField : IField
	{
		private const int TABLE_LENGTH_LENGTH = 3;

		private IField tableEntry;

		public int Length { get { return this.tableEntry.ToString().Length + TABLE_LENGTH_LENGTH; } }
		public AidIccEmvEntry AidIccEmv { get { return this.tableEntry as AidIccEmvEntry; } set { this.tableEntry = value; } }
		public AidEasyEntryTibcV1Entry AidEasyEntryTibcV1 { get { return this.tableEntry as AidEasyEntryTibcV1Entry; } set { this.tableEntry = value; } }
		public AidNullEntry AidNull { get { return this.tableEntry as AidNullEntry; } set { this.tableEntry = value; } }
		public CapkEntry Capk { get { return this.tableEntry as CapkEntry; } set { this.tableEntry = value; } }
		public RevokedCertificateEntry RevokedCertificate { get { return this.tableEntry as RevokedCertificateEntry; } set { this.tableEntry = value; } }

		public void Init(StringReader stringReader)
		{
			var contentLength = stringReader.Read(TABLE_LENGTH_LENGTH).ConvertTo<int>() - TABLE_LENGTH_LENGTH;

			var content = stringReader.Read(contentLength);
			var contentReader = new StringReader(content);

			try
			{
				this.tableEntry = new AidIccEmvEntry();
				this.tableEntry.Init(contentReader);
			}
			catch
			{
				try
				{
					contentReader.Seek(-contentReader.Offset);
					this.tableEntry = new AidEasyEntryTibcV1Entry();
					this.tableEntry.Init(contentReader);
				}
				catch
				{
					try
					{
						contentReader.Seek(-contentReader.Offset);
						this.tableEntry = new AidNullEntry();
						this.tableEntry.Init(contentReader);
					}
					catch
					{
						try
						{
							contentReader.Seek(-contentReader.Offset);
							this.tableEntry = new CapkEntry();
							this.tableEntry.Init(contentReader);
						}
						catch
						{
							contentReader.Seek(-contentReader.Offset);
							this.tableEntry = new RevokedCertificateEntry();
							this.tableEntry.Init(contentReader);
						}
					}
				}
			}
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Length.ToString(TABLE_LENGTH_LENGTH));
			stringBuilder.Append(this.tableEntry.ToString());

			return stringBuilder.ToString();
		}
	}
}
