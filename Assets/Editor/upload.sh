#!/bin/bash

source ../../../config.sh

project="${1}"
version="${2}"
buildPath="${3}"
buildFilename="${4}"
encodedBuildFilename="${5}"
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
    -d '{"tag_name": "'${version}'", "name":"'${version}'","generate_release_notes":true}' \
    https://api.github.com/repos/${repo}/releases`
}

createRelease()
{
  echo `curl \
    -X POST \
    -H "Authorization: token ${token}"  \
    -H "Accept: application/vnd.github.v3+json" \
    -d '{"tag_name": "'${version}'", "name":"'${version}'","generate_release_notes":true}' \
    https://api.github.com/repos/${repo}/releases`
}

createMockRelease()
{
  echo '{ "url": "https://api.github.com/repos/Fried-Synapse/FlowEnt/releases/54175066", "assets_url": "https://api.github.com/repos/Fried-Synapse/FlowEnt/releases/54175066/assets", "upload_url": "https://uploads.github.com/repos/Fried-Synapse/FlowEnt/releases/54175066/assets{?name,label}", "html_url": "https://github.com/Fried-Synapse/FlowEnt/releases/tag/v1.1.3", "id": 54175066, "author": { "login": "danieltranca", "id": 80676873, "node_id": "MDQ6VXNlcjgwNjc2ODcz", "avatar_url": "https://avatars.githubusercontent.com/u/80676873?v=4", "gravatar_id": "", "url": "https://api.github.com/users/danieltranca", "html_url": "https://github.com/danieltranca", "followers_url": "https://api.github.com/users/danieltranca/followers", "following_url": "https://api.github.com/users/danieltranca/following{/other_user}", "gists_url": "https://api.github.com/users/danieltranca/gists{/gist_id}", "starred_url": "https://api.github.com/users/danieltranca/starred{/owner}{/repo}", "subscriptions_url": "https://api.github.com/users/danieltranca/subscriptions", "organizations_url": "https://api.github.com/users/danieltranca/orgs", "repos_url": "https://api.github.com/users/danieltranca/repos", "events_url": "https://api.github.com/users/danieltranca/events{/privacy}", "received_events_url": "https://api.github.com/users/danieltranca/received_events", "type": "User", "site_admin": false }, "node_id": "RE_kwDOFOoxcc4DOqVa", "tag_name": "v1.1.3", "target_commitish": "main", "name": "v1.1.3", "draft": false, "prerelease": false, "created_at": "2021-11-27T12:30:39Z", "published_at": "2021-11-27T17:47:47Z", "assets": [ ], "tarball_url": "https://api.github.com/repos/Fried-Synapse/FlowEnt/tarball/v1.1.3", "zipball_url": "https://api.github.com/repos/Fried-Synapse/FlowEnt/zipball/v1.1.3", "body": "**Full Changelog**: https://github.com/Fried-Synapse/FlowEnt/compare/v1.1.2...v1.1.3" }'
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
    https://uploads.github.com/repos/${repo}/releases/${releaseId}/assets?name=${encodedBuildFilename}&label=${encodedBuildFilename}`
}

responseCreate="$(createRelease)"
echo "Created release with response: \n${responseCreate}"

responseCreate=$(minifyJson "$responseCreate")

uploadUrl=$(${jqPath} -r '.upload_url' <<< ${responseCreate})
releaseId=$(${jqPath} -r '.id' <<< ${responseCreate})

responseUpload="$(uploadAsset "$releaseId")"
echo "Uploaded package with response: \n${responseUpload}"