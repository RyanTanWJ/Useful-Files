package com.ryan.user.service.ryanuserservice.model.response;

public abstract class BaseResponse {
    protected boolean success;
    protected String message;

    public boolean isSuccess() {
        return success;
    }

    public void setSuccess(boolean success) {
        this.success = success;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
