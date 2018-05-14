<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt"
  xmlns:catalog="http://library.by/catalog"
  exclude-result-prefixes="msxsl">

  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/catalog:catalog">
    <rss version="2.0">
      <channel>
        <title>New books received</title>
        <link>http://library.by/catalog</link>
        <description>These and other books are now available to order</description>
        <xsl:apply-templates/>
      </channel>
    </rss>
  </xsl:template>
  
  <xsl:template match="/catalog:catalog/catalog:book">
    <item>
      <xsl:copy-of select="catalog:author"/>
      <xsl:copy-of select="catalog:title"/>
      
      <xsl:if test="(catalog:genre='Computer') and (catalog:isbn)">
        <link>
          <xsl:value-of select="concat('http://my.safaribooksonline.com/', catalog:isbn, '/')"/>
        </link>
      </xsl:if>
      
      <pubDate>
        <xsl:value-of select="catalog:registration_date"/>
      </pubDate>
      
      <xsl:copy-of select="catalog:description"/>
    </item>
  </xsl:template>

</xsl:stylesheet>
