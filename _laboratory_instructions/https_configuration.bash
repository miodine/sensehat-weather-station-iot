#!/bin/bash
# Change directory: Lighttpd app main directory
cd "/etc/lighttpd/"
# Create new directory for your certificate 
sudo mkdir certificates
# Change directory: new certificate subdirectory
cd "certificates/"
# Run OpenSSL app
DOMAIN="raspberrypi"
sudo openssl req -new -x509 -keyout $DOMAIN.pem -out $DOMAIN.pem -days 365 -nodes
# Change file owner and add read/write permissons 
sudo chown www-data:www-data $DOMAIN.pem
sudo chmod a+rw $DOMAIN.pem
# Enable SSL support in Lighttpd
sudo lighty-enable-mod ssl
# Edit configuration file
cd /etc/lighttpd/conf-enabled/
  ## FILE CONTENT:
  #
  # $SERVER["socket"] == "0.0.0.0:443" {
  #  ssl.engine  = "enable"
  #  ssl.pemfile = "/etc/lighttpd/certificates/raspberrypi.pem"
  #  ssl.cipher-list = "HIGH"
  # }
  #
sudo nano 10-ssl.conf
# Restart Lighttpd 
sudo service lighttpd restart
# Check your client applications with 'https'