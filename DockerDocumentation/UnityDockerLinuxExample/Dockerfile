FROM registry.access.redhat.com/ubi8/dotnet-31-runtime

COPY headless-test_Data ./root/headless-test_Data
COPY headless-test.x86_64 ./root

ENTRYPOINT ./root/headless-test.x86_64 -ab 12312 --cdqe v33554ggte