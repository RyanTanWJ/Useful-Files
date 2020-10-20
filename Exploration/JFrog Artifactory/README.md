# JFrog Artifactory
This page serves to document my exploration of JFrog Artifactory.

## Chapters
1. What is JFrog Artifactory?
2. Installing JFrog Artifactory
3. JFrog Academy
4. Re-evaluating my Use Case
5. The Meeting
6. JFrog Products and Services
7. Final Evaluation

# What is JFrog Artifactory?
JFrog Artifactory is a artifact (binary) manager. It both stores and manages the binaries created from building a project or the dependencies used in a project.

# Installing JFrog Artifactory
Installing JFrog Artifactory was easy. Following the instructions on provided on the website allowed me to quickly install JFrog on my computer. I had a limited time to explore the JFrog services as I was using the trial version however, and I was unsure what to do. After some exploring, I found a link to the JFrog Academy to learn more about Artifactory.

# JFrog Academy
I started from the first lesson on JFrog Artifactory and learned that there were multiple types of containers.

# Re-evaluating my Use Case


# The Meeting


# JFrog Products and Services
During the meeting, I was introduced to other JFrog products and services as well. Of note are:

## JFrog Pipelines
Can be used to configure builds using YAML file. Similar to GitLab CI/CD.

## JFrog X-Ray
Can be used to identify known vulnerabilities in packages. Also identifies builds that used vulnerable packages.

# Final Evaluation
As storing binaries would take up a lot of space on my server, I decided against using JFrog Artifactory as I already have a Git history of the code base and can rebuild an old build when necessary.