#!/bin/sh
curl -sS -X POST "${RXFLOW_DOTNET_BASE_URL:-http://localhost:18081}/orders" -H 'Authorization: Bearer training-token' -H 'Content-Type: application/json' -d '{"patientId":"SYN-1042","frameSku":"F-220","sphere":-2.25,"cylinder":-1.25,"axis":92,"material":"POLYCARBONATE","coating":"AR"}'
