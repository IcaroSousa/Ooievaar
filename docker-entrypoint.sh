#!/bin/bash

[[ -z "${REDIS_HOST}" ]] && redis_host='localhost' || redis_host="${REDIS_HOST}"
[[ -z "${REDIS_PORT}" ]] && redis_port='6379' || redis_port="${REDIS_PORT}"
[[ -z "${REDIS_NAMESPACE}" ]] && redis_namespace='MP' || redis_namespace="${REDIS_NAMESPACE}"
[[ -z "${REDIS_QUEUENAME}" ]] && redis_queuename='Ooievaar' || redis_queuename="${REDIS_QUEUENAME}"

echo \{ >config.json
echo $'\t'\"redis_namespace\"\: \"${redis_namespace}\", >>config.json
echo $'\t'\"redis_queuename\"\: \"${redis_queuename}\", >>config.json
echo $'\t'\"redis_host\"\: \"${redis_host}\", >>config.json
echo $'\t'\"redis_port\"\: \"${redis_port}\" >>config.json
echo \} >>config.json