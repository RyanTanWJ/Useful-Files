package com.ryan.user.service.ryanuserservice.model.response;

public class DeleteUserResponse extends BaseResponse {
    private String userId;
    private String name;
    private String userName;

    public DeleteUserResponse(String userId, String name, String userName) {
        this.userId = userId;
        this.name = name;
        this.userName = userName;
        this.success = true;
        this.message = "user successfully deleted";
    }

    public DeleteUserResponse(String userId, String name, String userName, String message) {
        this.userId = userId;
        this.name = name;
        this.userName = userName;
        this.success = true;
        this.message = message;
    }

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    @Override
    public String toString() {
        return "DeleteUserResponse{" +
                "success=" + success +
                ", message='" + message + '\'' +
                ", userId='" + userId + '\'' +
                ", name='" + name + '\'' +
                ", userName='" + userName + '\'' +
                '}';
    }
}
