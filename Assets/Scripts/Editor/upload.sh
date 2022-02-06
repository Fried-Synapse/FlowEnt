#!/bin/bash

source ../../../../config.sh

project="${1}"
buildPath="${2}"
buildFilename="${3}"
organisation="Fried-Synapse"
repo="${organisation}/${project}"

minifyJson()
{
  local json="$1"
  echo "${1//[$'\t\r\n']}"
}

getReleases()
{
  echo `curl \
    -X GET \
    -H "Authorization: token ${token}"  \
    -H "Accept: application/vnd.github.v3+json" \
    https://api.github.com/repos/${repo}/releases`
}

createRelease()
{
  echo `curl \
    -X POST \
    -H "Authorization: token ${token}"  \
    -H "Accept: application/vnd.github.v3+json" \
    -d '{ "tag_name": "'${version}'", "name":"'${version}'","generate_release_notes":true, "prerelease":false }' \
    https://api.github.com/repos/${repo}/releases`
}

uploadAsset()
{
  local releaseId="$1"
  echo `curl \
    -X POST \
    -H "Authorization: token ${token}"  \
    -H "Accept: application/vnd.github.v3+json" \
    -H "Content-Type: application/zip" \
    --data-binary "@${buildPath}/${buildFilename}"  \
    https://uploads.github.com/repos/${repo}/releases/${releaseId}/assets?name=${buildFilename}&label=${buildFilename}`
}

responseCreate="$(createRelease)"
echo "Created release with response: \n${responseCreate}"

responseCreate=$(minifyJson "$responseCreate")

uploadUrl=$(${jqPath} -r '.upload_url' <<< ${responseCreate})
releaseId=$(${jqPath} -r '.id' <<< ${responseCreate})

responseUpload="$(uploadAsset "$releaseId")"
echo "Uploaded package with response: \n${responseUpload}"