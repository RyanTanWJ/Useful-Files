package com.ryan.user.service.ryanuserservice.model.response;

public abstract class BaseResponse {
    protected boolean success;

    public boolean isSuccess() {
        return success;
    }

    public void setSuccess(boolean success) {
        this.success = success;
    }
}
