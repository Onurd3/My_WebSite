using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
	public enum EPaymentOption
	{
		[Display(Name="Kredi Kartı ile Ödeme")]
		KrediKartı = 1,
		[Display(Name = "Havale/EFT ile Ödeme")]
		Havale,
		[Display(Name = "Kapıda Nakit/Kredi Kartı ile Ödeme")]
		KapıdaOdeme
	}
}
