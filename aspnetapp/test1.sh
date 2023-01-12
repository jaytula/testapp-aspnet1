#!/bin/bash

openssl rsautl -encrypt -inkey id_rsa -in data.txt -out encrypted_data.bin
curl -X POST -H 'Content-Type: application/octet-stream' --data-binary "@encrypted_data.bin" http://localhost:8080/AuthLogin