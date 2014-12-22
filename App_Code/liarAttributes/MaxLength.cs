using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// MaxLength 的摘要描述
/// </summary>
public class MaxLength:Attribute
{
    public int m_maxLength { get; set; }

	public MaxLength()
	{
	}

    public MaxLength(int m_maxLength) {
        this.m_maxLength = m_maxLength;
    }
}